#ifndef ISCREENGRABBER_H
#define ISCREENGRABBER_H

#include "MediaRecordTypeDef.h"

namespace MediaFileRecorder {

	class IScreenGrabberDataCb
	{
	public:
		virtual ~IScreenGrabberDataCb(){};

		// RGB data callback
		virtual void OnScreenData(void* data, const VIDEO_INFO& videoInfo) = 0;
	};

	class IScreenGrabber
	{
	public:
		virtual int RegisterDataCb(IScreenGrabberDataCb* cb) = 0;
		virtual int UnRegisterDataCb(IScreenGrabberDataCb* cb) = 0;

		virtual int SetGrabRect(const RECT& rect) = 0;
		virtual int SetGrabFrameRate(int frame_rate) = 0;

		virtual int StartGrab() = 0;
		virtual int StopGrab() = 0;

		virtual ~IScreenGrabber(){}
	};


	IScreenGrabber* CreateScreenGrabber();
	void DestroyScreenGrabber(IScreenGrabber* pScreenGrabber);
	
}
#endif