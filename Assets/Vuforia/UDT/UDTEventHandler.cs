using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

public class UDTEventHandler : MonoBehaviour, IUserDefinedTargetEventHandler
{
	public ImageTargetBehaviour ImageTargetTemplate;
	const int MAX_TARGETS = 3; //Cantidad maxima de instancias
	int m_TargetCounter;

	public int LastTargetIndex {
        get { return (m_TargetCounter - 1) % MAX_TARGETS; }
    }

	UserDefinedTargetBuildingBehaviour m_TargetBuildingBehaviour;
    ObjectTracker m_ObjectTracker;
    UDTFrameQualityMeter m_FrameQualityMeter;
    DataSet m_UDT_DataSet;
    ImageTargetBuilder.FrameQuality m_FrameQuality = ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE;


    void Start() {
		m_FrameQualityMeter = FindObjectOfType<UDTFrameQualityMeter> ();
		m_TargetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>();

        if (m_TargetBuildingBehaviour) {
            m_TargetBuildingBehaviour.RegisterEventHandler(this);
        }
    }

    public void OnInitialized() {
        m_ObjectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (m_ObjectTracker != null) {
            m_UDT_DataSet = m_ObjectTracker.CreateDataSet();
            m_ObjectTracker.ActivateDataSet(m_UDT_DataSet);
        }
    }

    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality) {
        Debug.Log("Calidad cambiada: " + frameQuality.ToString());
        m_FrameQuality = frameQuality;
        if (m_FrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_LOW) {
            Debug.Log("Baja calidad de Imagen");
        }

        m_FrameQualityMeter.SetQuality(frameQuality);
    }

	//Se ejecuta cada vez que se crea una instancia
    public void OnNewTrackableSource(TrackableSource trackableSource) {
        m_TargetCounter++;
        m_ObjectTracker.DeactivateDataSet(m_UDT_DataSet);

		//Crea una instancia nueva y borra la mas vieja
        if (m_UDT_DataSet.HasReachedTrackableLimit() || m_UDT_DataSet.GetTrackables().Count() >= MAX_TARGETS) { 
			IEnumerable<Trackable> trackables = m_UDT_DataSet.GetTrackables();
            Trackable oldest = null;
            foreach (Trackable trackable in trackables) {
                if (oldest == null || trackable.ID < oldest.ID)
                    oldest = trackable;
            }

            if (oldest != null) {
                m_UDT_DataSet.Destroy(oldest, true);
            }
        }

        ImageTargetBehaviour imageTargetCopy = Instantiate(ImageTargetTemplate);
        imageTargetCopy.gameObject.name = "UserDefinedTarget-" + m_TargetCounter;

        m_UDT_DataSet.CreateTrackable(trackableSource, imageTargetCopy.gameObject);

        m_ObjectTracker.ActivateDataSet(m_UDT_DataSet);

        m_TargetBuildingBehaviour.StartScanning();
    }
   
	//Se crea una nueva instancia
    public void BuildNewTarget() {
        if (m_FrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_MEDIUM ||
            m_FrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH) {

            string targetName = string.Format("{0}-{1}", ImageTargetTemplate.TrackableName, m_TargetCounter);
            m_TargetBuildingBehaviour.BuildNewTarget(targetName, ImageTargetTemplate.GetSize().x);
        }
        else {
            Debug.Log("No se puede instanciar el objeto; poca calidad de Imagen");
        }
    }
    
}