; The following two lines require the Preprocessor plugin for Inno Setup. Or just download the Quick-Start pack: http://www.jrsoftware.org/isdl.php#qsp
#define SrcApp "bin\release\CaseTracker.exe"
#define FileVerStr GetFileVersion(SrcApp)

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{7FB91CB0-CEB7-42B3-AC96-B608B4C89E53}
AppName=Case Tracker
AppVerName=Case Tracker
AppPublisher=VisionMap Inc.
AppPublisherURL=http://www.visionmap.com/
AppSupportURL=http://www.visionmap.com/
AppUpdatesURL=http://www.visionmap.com/
DefaultDirName={pf}\CaseTracker
DefaultGroupName=Case Tracker
AllowNoIcons=yes
OutputBaseFilename=CaseTracker-{#FileVerStr}-setup
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Code]
procedure ClosePreviousVersion();
var
  oldHwnd: HWnd;
begin
  oldHwnd := FindWindowByWindowName('Case Tracker');
  if (oldHwnd <> 0) then
     SendMessage(oldHwnd, 16, 0, 0);             // WM_CLOSE

end;

procedure CreateDebugLink();
begin
  CreateShellLink(
    ExpandConstant('{app}\Debug-Mode Case Tracker.lnk'),
    'Run CaseTracker in debug mode (for troubleshooting)',
    ExpandConstant('{app}\CaseTracker.exe'),
    ExpandConstant('DEBUG'),
    ExpandConstant('{app}'),
    '',
    0,
    SW_SHOWNORMAL);
end;


[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}";
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}";

[Files]
Source: "{#SrcApp}"; DestDir: "{app}"; Flags: ignoreversion ; BeforeInstall: ClosePreviousVersion  ; AfterInstall: CreateDebugLink
Source: "{#SrcApp}.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\FogBugzNet\bin\release\FogBugzNet.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Log4Net\release\log4net.dll"; DestDir: "{app}"; Flags: ignoreversion

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\Case Tracker"; Filename: "{app}\CaseTracker.exe"
Name: "{commondesktop}\Case Tracker"; Filename: "{app}\CaseTracker.exe"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\Case Tracker"; Filename: "{app}\CaseTracker.exe"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\CaseTracker.exe"; Description: "{cm:LaunchProgram,Case Tracker}"; Flags: nowait postinstall

