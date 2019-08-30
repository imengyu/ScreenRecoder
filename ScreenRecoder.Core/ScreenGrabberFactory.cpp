
#include "stdafx.h"
#include "IScreenGrabber.h"
#include "CScreenGdiGrabber.h"
#include "CScreenDXGrabber.h"
#include "system_info.h"

namespace MediaFileRecorder
{
	IScreenGrabber* CreateScreenGrabber()
	{
		IScreenGrabber* pScreenGrabber = new CScreenGdiGrabber();
		//IScreenGrabber* pScreenGrabber = new CScreenDXGrabber();
		return pScreenGrabber;
	}

	void DestroyScreenGrabber(IScreenGrabber* pScreenGrabber)
	{
		delete pScreenGrabber;
	}
}