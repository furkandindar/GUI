
// project3.h : main header file for the project3 application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// Cproject3App:
// See project3.cpp for the implementation of this class
//

class Cproject3App : public CWinApp
{
public:
	Cproject3App();


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation

public:
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern Cproject3App theApp;
