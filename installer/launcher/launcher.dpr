program launcher;

uses
  Vcl.Forms,
  Aio,
  Greenlets,
  Classes,
  SyncObjs,
  ShellAPI,
  Windows,
  SysUtils,
  fmMain in 'fmMain.pas' {MainForm};

const
  EXTERNAL_PROGRAM = 'bin\Miner.exe';

var
  DotNetDetector: IAioConsoleApplication;
  Output, Arguments: TStringList;
  AsyncRead: TSymmetric;
  DotNetInstalled: Boolean;
  I: Integer;
  CurDir: string;


procedure RunExternalProgramDetached(Exe: string; Args: string);
var
  SEInfo: TShellExecuteInfo;
begin
  FillChar(SEInfo, SizeOf(SEInfo), 0) ;
  SEInfo.cbSize := SizeOf(TShellExecuteInfo) ;
  with SEInfo do begin
     fMask := SEE_MASK_NOCLOSEPROCESS;
     Wnd := Application.Handle;
     lpFile := PChar(Exe);
     lpParameters := PChar(Args);
     nShow := SW_SHOWNORMAL;
  end;
  ShellExecuteEx(@SEInfo)
end;

{$R *.RES}

begin
  Output := TStringList.Create;
  Arguments := TStringList.Create;
  DotNetDetector := MakeAioConsoleApp('NET_Detector_cli.exe', []);

  AsyncRead := TSymmetric.Spawn(procedure
    var
      S: string;
    begin
      for S in DotNetDetector.StdOut.ReadLns do begin
        Output.Add(S);
        if Pos('.NET Framework', S) > 0 then
          DotNetInstalled := True;
      end;
    end
  );
  Join([AsyncRead], 1000);

  if DotNetInstalled then begin
    for I := 1 to ParamCount do
      Arguments.Add(ParamStr(I));
    Arguments.Delimiter := ' ';
    RunExternalProgramDetached(GetCurrentDir + '\' + EXTERNAL_PROGRAM, Arguments.DelimitedText);
  end
  else begin
    Application.Initialize;
    Application.MainFormOnTaskbar := True;
    Application.CreateForm(TMainForm, MainForm);
    Application.Run;
  end;
end.
