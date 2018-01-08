// Win32Draw.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "Win32Draw.h"
#include <time.h>
#include <iostream>
using namespace std;
#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name
bool flagDrawCircle = false;
bool flagDrawRect = false;
// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

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
    LoadStringW(hInstance, IDC_WIN32DRAW, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Perform application initialization:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_WIN32DRAW));

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

    return (int) msg.wParam;
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

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_WIN32DRAW));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_WIN32DRAW);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

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

   HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      0, 0, 1000, 1000, nullptr, nullptr, hInstance, nullptr);
   srand(time(NULL));

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
    switch (message)
    {
    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // Parse the menu selections:
            switch (wmId)
            {
            case IDM_ABOUT:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                break;
            case IDM_EXIT:
                DestroyWindow(hWnd);
                break;
			case ID_CIRCLE:
				flagDrawCircle = TRUE;
				flagDrawRect = FALSE;
				InvalidateRect(hWnd,0,FALSE);
				break;
			case ID_RECT:
				flagDrawRect = TRUE;
				flagDrawCircle = FALSE;
				InvalidateRect(hWnd, 0, FALSE);
				break;
			case ID_CLEARSCREEN:
				InvalidateRect(hWnd, 0, TRUE);
				flagDrawRect = FALSE;
				flagDrawCircle = FALSE;
				break;
			case ID_DRAW:
				flagDrawRect = TRUE;
				flagDrawCircle = TRUE;
				break;
            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;
    case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
			hdc = GetDC(hWnd);
			if (flagDrawCircle) { // Draws circle if selected
				HPEN hPen = CreatePen(PS_SOLID, 3, RGB(0, 0, 255));
				HBRUSH hBrush = CreateHatchBrush(HS_DIAGCROSS, RGB(255, 0, 255));
				HPEN hOldPen = (HPEN)SelectObject(hdc, hPen);
				HBRUSH hOldBrush = (HBRUSH)SelectObject(hdc, hBrush);
				int x = rand() % 780;
				int y = rand() % 780;
				int z = rand() % 200 + 20;
				Ellipse(hdc, x, y, x + z, y + z);
				SelectObject(hdc, hOldBrush);          
				DeleteObject(hBrush);                 
				SelectObject(hdc, hOldPen);            
				DeleteObject(hPen);
				ReleaseDC(hWnd, hdc);
			}
			if (flagDrawRect) { // draws rectange if selected
				HPEN hPenRect = CreatePen(PS_SOLID, 3, RGB(255, 0, 0));
				HBRUSH hBrushRect = CreateSolidBrush(RGB(0, 229, 238));
				HPEN hOldPenRect = (HPEN)SelectObject(hdc, hPenRect);
				HBRUSH hOldBrushRect = (HBRUSH)SelectObject(hdc, hBrushRect);
				int a = rand() % 780;
				int b = rand() % 780;
				int c = rand() % 200 + 20;
				int d = rand() % 200 + 20;
				Rectangle(hdc, a, b, a + c, b + d);
				SelectObject(hdc, hOldBrushRect);
				DeleteObject(hBrushRect);
				SelectObject(hdc, hOldPenRect);
				DeleteObject(hPenRect);
				ReleaseDC(hWnd, hdc);
			}
			EndPaint(hWnd, &ps);
        }
        break;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
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
