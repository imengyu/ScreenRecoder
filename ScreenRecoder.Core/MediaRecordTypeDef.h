#ifndef MEDIARECORDTYPEDEF_H
#define MEDIARECORDTYPEDEF_H

namespace MediaFileRecorder
{
	enum PIX_FMT
	{
		PIX_FMT_UNKOWN = 0,
		PIX_FMT_RGB24,
		PIX_FMT_BGR24,
		PIX_FMT_ARGB,
		PIX_FMT_BGRA,
	};

	enum AUDIO_FORMAT
	{
		AUDIO_FORMAT_UNKNOWN = 0,

		AUDIO_FORMAT_U8BIT,
		AUDIO_FORMAT_16BIT,
		AUDIO_FORMAT_32BIT,
		AUDIO_FORMAT_FLOAT,

		AUDIO_FORMAT_U8BIT_PLANAR,
		AUDIO_FORMAT_16BIT_PLANAR,
		AUDIO_FORMAT_32BIT_PLANAR,
		AUDIO_FORMAT_FLOAT_PLANAR,
	};

	enum CHANNEL_LAYOUT
	{
		SPEAKERS_UNKNOWN = 0,
		SPEAKERS_MONO,
		SPEAKERS_STEREO,
		SPEAKERS_2POINT1,
		SPEAKERS_QUAD,
		SPEAKERS_4POINT1,
		SPEAKERS_5POINT1,
		SPEAKERS_5POINT1_SURROUND,
		SPEAKERS_7POINT1,
		SPEAKERS_7POINT1_SURROUND,
		SPEAKERS_SURROUND,
	};

	enum VIDEO_QUALITY
	{
		UNKOWN = 0,
		NORMAL,
		HIGH,
		VERY_HIGH
	};

	enum VIDEO_FORMAT
	{
		VIDEO_FORMAT_UNKOWN = 0,
		VIDEO_FORMAT_H264_MP4,
		VIDEO_FORMAT_MPEG4_MP4,
		VIDEO_FORMAT_H264_AVI,
		VIDEO_FORMAT_MPEG4_AVI,
		VIDEO_FORMAT_FLV,
		VIDEO_FORMAT_WMV
	};

	enum RECORD_START_RESULT
	{
		STATE_NOT_RIGHT = 0x1,
		PARAMETER_INVALID = 0x2,
		START_SCRREEN_CAPTURE_FAILED = 0x4,
		START_MIC_CAPTURE_FAILED = 0x8,
		START_SPEAKER_CAPTURE_FAILED = 0x10,
		INIT_MEDIA_FILE_RECORDER_FAILED = 0x20,
		START_MEDIA_FILE_RECORDER_FAILED = 0x40
	};

	struct VIDEO_INFO
	{
		int width;
		int height;
		PIX_FMT pix_fmt;
		VIDEO_INFO()
		{
			Reset();
		}

		void Reset()
		{
			memset(this, 0, sizeof(VIDEO_INFO));
		}
	};

	struct AUDIO_INFO
	{
		int sample_rate;
		AUDIO_FORMAT audio_format;
		CHANNEL_LAYOUT chl_layout;
		AUDIO_INFO()
		{
			Reset();
		}

		void Reset()
		{
			memset(this, 0, sizeof(AUDIO_INFO));
		}
	};

	struct RECT
	{
		int left;
		int right;
		int top;
		int bottom;
		RECT()
		{
			left = right = top = bottom = 0;
		}
		int Width() const
		{
			return right - left;
		}
		int Height() const
		{
			return bottom - top;
		}
	};

	struct RECORD_INFO
	{
		char file_name[1024];
		BOOL is_record_video;
		BOOL is_record_mic;
		BOOL is_record_speaker;
		RECT video_capture_rect;
		int  video_dst_width;
		int  video_dst_height;
		int  video_frame_rate;
		VIDEO_QUALITY quality;
		VIDEO_FORMAT format;
		RECORD_INFO()
		{
			memset(file_name, 0, 1024);
			is_record_video = false;
			is_record_mic = false;
			is_record_speaker = false;

			video_dst_width = 0;
			video_dst_height = 0;
			quality = UNKOWN;
			format = VIDEO_FORMAT_H264_MP4;
		}
	};


	enum SDK_LOG_LEVEL
	{
		LOG_DEBUG = 0,
		LOG_INFO,
		LOG_WARNING,
		LOG_ERROR
	};

	typedef void(__cdecl *sdk_log_cb_t)(SDK_LOG_LEVEL level, const wchar_t* msg);
}
#endif