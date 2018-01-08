
// MainFrm.cpp : implementation of the CMainFrame class
//

#include "stdafx.h"
#include "project3.h"

#include "MainFrm.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CMainFrame

IMPLEMENT_DYNAMIC(CMainFrame, CFrameWnd)

BEGIN_MESSAGE_MAP(CMainFrame, CFrameWnd)
	ON_WM_PAINT()
	ON_WM_MOVE()
END_MESSAGE_MAP()

static UINT indicators[] =
{
	ID_SEPARATOR,           // status line indicator
	ID_INDICATOR_CAPS,
	ID_INDICATOR_NUM,
	ID_INDICATOR_SCRL,
};

// CMainFrame construction/destruction

CMainFrame::CMainFrame()
{
	// TODO: add member initialization code here
}

CMainFrame::~CMainFrame()
{
}

void CMainFrame::OnPaint() {
	CPaintDC dc(this);
	CString hello = _T("Hello from Furkan Dindar");
	dc.TextOutW(1, 1, hello);

	CDC *cdc;
	cdc = GetDC();
	RECT rc;
	GetWindowRect(&rc);
	CString left, right, top, bottom;
	left.Format(L"%d",rc.left);
	right.Format(L"%d",rc.right);
	top.Format(L"%d",rc.top);
	bottom.Format(L"%d",rc.bottom);
	
	(*cdc).TextOut(1,20, L"LEFT: " + left + L"     RIGHT: " + right + L"     BOTTOM: " + bottom + L"     TOP: " + top);
	(*cdc).TextOut(1, 40, L"LEFT: " + left + L"     RIGHT: " + right + L"     BOTTOM: " + bottom + L"     TOP: " + top + L"  ");
}

void CMainFrame::OnMove(int i,int j) {
	CDC *cdc;
	cdc = GetDC();
	CRect rc;
	GetWindowRect(&rc);
	CString left, right, top, bottom;
	left.Format(L"%d", rc.left);
	right.Format(L"%d", rc.right);
	top.Format(L"%d", rc.top);
	bottom.Format(L"%d", rc.bottom);

	int t;
	srand(time(NULL));
	t = rand() % 5;
	if (t == 0) {
		(*cdc).SetTextColor(RGB(255, 0, 0));
	}
	else if (t == 1) {
		(*cdc).SetTextColor(RGB(0, 255, 0));
	}
	else if (t == 2) {
		(*cdc).SetTextColor(RGB(0, 0, 255));
	}
	else if (t == 3) {
		(*cdc).SetTextColor(RGB(255, 239, 0));
	}
	else {
		(*cdc).SetTextColor(RGB(255, 145, 0));
	}
	(*cdc).TextOut(1, 40, L"LEFT: " + left + L"     RIGHT: " + right + L"     BOTTOM: " + bottom + L"     TOP: " + top + L"  ");
	(*cdc).TextOut(1, 20, L"LEFT: " + left + L"     RIGHT: " + right + L"     BOTTOM: " + bottom + L"     TOP: " + top + L"  ");
}


BOOL CMainFrame::PreCreateWindow(CREATESTRUCT& cs)
{
	if( !CFrameWnd::PreCreateWindow(cs) )
		return FALSE;
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	cs.dwExStyle &= ~WS_EX_CLIENTEDGE;
	cs.lpszClass = AfxRegisterWndClass(0);
	return TRUE;
}

// CMainFrame diagnostics

#ifdef _DEBUG
void CMainFrame::AssertValid() const
{
	CFrameWnd::AssertValid();
}

void CMainFrame::Dump(CDumpContext& dc) const
{
	CFrameWnd::Dump(dc);
}
#endif //_DEBUG


// CMainFrame message handlers

void CMainFrame::OnSetFocus(CWnd* /*pOldWnd*/)
{
	// forward focus to the view window
	m_wndView.SetFocus();
}

BOOL CMainFrame::OnCmdMsg(UINT nID, int nCode, void* pExtra, AFX_CMDHANDLERINFO* pHandlerInfo)
{
	// let the view have first crack at the command
	if (m_wndView.OnCmdMsg(nID, nCode, pExtra, pHandlerInfo))
		return TRUE;

	// otherwise, do default handling
	return CFrameWnd::OnCmdMsg(nID, nCode, pExtra, pHandlerInfo);
}

