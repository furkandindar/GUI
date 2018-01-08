// Project1.cpp : Defines the entry point for the application.
// tried to implement scrolldown but I couldn't, instead of deleting all the codes for it, I just commented them out

#include "stdafx.h"
#include "Project1.h"
#include <time.h>
#include <Windows.h>
#include <stdlib.h>
#include <tchar.h>

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[] = _T("Furkan Dindar");                  // The title bar text
WCHAR szWindowClass[] = _T("Furkan Dindar");            // the main window class name
int nHeight; int PointSize = 18;
//static int yCurrentScroll; static int yChar; static int yClient; int FirstLine; int LastLine; int y;
static int y_position = 5;
static int line_num = 0;
HFONT hTmp;
unsigned int r1, r2, r3;
// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
void set_font(HDC hDC);
void increment_line_num(HDC hDC);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// TODO: Place code here.

	// Initialize global strings
	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadStringW(hInstance, IDC_PROJECT1, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance(hInstance, nCmdShow))
	{
		return FALSE;
	}

	HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_PROJECT1));

	MSG msg;

	// Main message loop:
	while (GetMessage(&msg, nullptr, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int)msg.wParam;
}



//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEXW wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_PROJECT1));
	wcex.hCursor = LoadCursor(nullptr, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wcex.lpszMenuName = MAKEINTRESOURCEW(IDC_PROJECT1);
	wcex.lpszClassName = szWindowClass;
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassExW(&wcex);
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	hInst = hInstance; // Store instance handle in our global variable

	HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW | WS_VSCROLL,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

	if (!hWnd)
	{
		return FALSE;
	}

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	return TRUE;
}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_COMMAND  - process the application menu
//  WM_PAINT    - Paint the main window
//  WM_DESTROY  - post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	//TEXTMETRIC tm; SCROLLINFO si; 
	PAINTSTRUCT ps; HDC hDC; TCHAR greeting[] = _T("Hello from Furkan"); TCHAR hello[] = _T("hello"); RECT rect;

	switch (message)
	{
		/*case WM_CREATE:
		hDC = GetDC(hWnd);
		GetTextMetrics(hDC, &tm);
		yChar = tm.tmHeight + tm.tmExternalLeading;
		ReleaseDC(hWnd, hDC);
		break;
		case WM_SIZE:
		yClient = HIWORD(lParam);
		si.cbSize = sizeof(si);
		si.fMask = SIF_RANGE | SIF_PAGE;
		si.nMin = 0;
		si.nMax = line_num - 1;
		si.nPage = yClient / yChar;
		SetScrollInfo(hWnd, SB_VERT, &si, TRUE);
		break;*/
	case WM_COMMAND:
	{
		int wmId = LOWORD(wParam);
		switch (wmId)
		{
		case IDM_ABOUT:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
			break;
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
	}
	break;
	case WM_PAINT:
	{
		hDC = BeginPaint(hWnd, &ps);
		nHeight = -MulDiv(PointSize, GetDeviceCaps(hDC, LOGPIXELSY), 72);
		GetClientRect(hWnd, &rect);
		set_font(hDC);
		SetTextColor(hDC, RGB(255, 0, 0));
		TextOut(hDC, 5, y_position, greeting, _tcslen(greeting));
		line_num++;
		/*si.cbSize = sizeof(si);
		si.fMask = SIF_POS;
		GetScrollInfo(hWnd, SB_VERT, &si);
		yCurrentScroll = si.nPos;

		FirstLine = max(0, yCurrentScroll + ps.rcPaint.top / yChar);
		LastLine = min(line_num - 1, yCurrentScroll + ps.rcPaint.bottom / yChar);
		for (int i = FirstLine; i <= LastLine; i++) {
		y = yChar * (i - yCurrentScroll);
		}*/
		EndPaint(hWnd, &ps);
		DeleteObject(SelectObject(hDC, hTmp));
	}
	break;

	case WM_LBUTTONDOWN:
	{
		InvalidateRect(hWnd, NULL, FALSE);
		hDC = BeginPaint(hWnd, &ps);
		set_font(hDC);
		if (line_num != 0) {
			increment_line_num(hDC);
		}
		TextOut(hDC, 5, y_position, hello, _tcslen(hello));
		EndPaint(hWnd, &ps);
	}
	break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
		/*case WM_VSCROLL:
		si.cbSize = sizeof(si);
		si.fMask = SIF_ALL;
		GetScrollInfo(hWnd,SB_VERT,&si);

		yCurrentScroll = si.nPos;
		switch (LOWORD(wParam)) {
		case SB_PAGEUP:
		si.nPos -= si.nPage;
		break;
		case SB_PAGEDOWN:
		si.nPos += si.nPage;
		break;
		case SB_THUMBTRACK:
		si.nPos = si.nTrackPos;
		break;
		default:
		break;
		}
		si.fMask = SIF_POS;
		SetScrollInfo(hWnd, SB_VERT, &si, TRUE);
		GetScrollInfo(hWnd, SB_VERT, &si);

		if (si.nPos != yCurrentScroll) {
		ScrollWindow(hWnd, 0, yChar * (yCurrentScroll - si.nPos), NULL, NULL);
		UpdateWindow(hWnd);
		}
		break;*/
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

void increment_line_num(HDC hDC) {
	line_num++;
	y_position = y_position + 30;
}

void set_font(HDC hDC) {
#define ul TRUE
#define xout TRUE
#define italic TRUE
#define no_ul FALSE
#define no_xout FALSE
#define no_italic FALSE
#define closest_match 0
#define escapement 0
#define orientation 0

	HFONT hFont, hTmp;
	//srand((unsigned int)time(NULL)); // with this line everytime we run the application we will get different colors, but if we click so fast all the hello colors look alike for around 5 lines, so I just commented it because we should wait long enough to get clearly different colors in every lines. Now everytime we run the application we will get same colors but all the hello lines have different colors
	r1 = rand() % 256; r2 = rand() % 256; r3 = rand() % 256;
	SetTextColor(hDC, RGB(r1, r2, r3));
	if (line_num % 5 == 0) {
		hFont = CreateFont(nHeight, closest_match, escapement, orientation, FW_DONTCARE, no_italic, no_ul, no_xout, ANSI_CHARSET, OUT_TT_ONLY_PRECIS, CLIP_DEFAULT_PRECIS, DRAFT_QUALITY, VARIABLE_PITCH, TEXT("Magneto"));
	}
	else if (line_num % 5 == 1) {
		hFont = CreateFont(nHeight, closest_match, escapement, orientation, FW_DONTCARE, no_italic, no_ul, no_xout, ANSI_CHARSET, OUT_TT_ONLY_PRECIS, CLIP_DEFAULT_PRECIS, DRAFT_QUALITY, VARIABLE_PITCH, TEXT("Algerian"));
	}
	else if (line_num % 5 == 2) {
		hFont = CreateFont(nHeight, closest_match, escapement, orientation, FW_DONTCARE, no_italic, no_ul, no_xout, ANSI_CHARSET, OUT_TT_ONLY_PRECIS, CLIP_DEFAULT_PRECIS, DRAFT_QUALITY, VARIABLE_PITCH, TEXT("Ravie"));
	}
	else if (line_num % 5 == 3) {
		hFont = CreateFont(nHeight, closest_match, escapement, orientation, FW_DONTCARE, no_italic, no_ul, no_xout, ANSI_CHARSET, OUT_TT_ONLY_PRECIS, CLIP_DEFAULT_PRECIS, DRAFT_QUALITY, VARIABLE_PITCH, TEXT("Blackadder ITC"));
	}
	else {
		hFont = CreateFont(nHeight, closest_match, escapement, orientation, FW_DONTCARE, no_italic, no_ul, no_xout, ANSI_CHARSET, OUT_TT_ONLY_PRECIS, CLIP_DEFAULT_PRECIS, DRAFT_QUALITY, VARIABLE_PITCH, TEXT("Curlz MT"));
	}
	hTmp = (HFONT)SelectObject(hDC, hFont);
	SetBkMode(hDC, TRANSPARENT);

}
// Message handler for about box.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}