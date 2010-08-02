Add-Type @"
public class Shift {
  public static int   Right(int x,   int count) { return x >> count; }
  public static uint  Right(uint x,  int count) { return x >> count; }
  public static long  Right(long x,  int count) { return x >> count; }
  public static ulong Right(ulong x, int count) { return x >> count; }
  public static int    Left(int x,   int count) { return x << count; }
  public static uint   Left(uint x,  int count) { return x << count; }
  public static long   Left(long x,  int count) { return x << count; }
  public static ulong  Left(ulong x, int count) { return x << count; }
}                    
"@

function Get-Git-Commit
{
	$gitLog = git log --oneline -1
	$tmpString = $gitLog.Split('m') 
	$index = 0
	if ($tmpString.Length -gt 2)
	{
		$index=1
	} else 
	{
		$tmpString = $gitLog.Split('') 
	}
		
	return $tmpString[$index].SubString(0,6)
}

function Generate-Revision
{	
param
(
	$startyear=2009
)
	$now = [DateTime]::Now
	
    $years = [Shift]::Left($now.Year-$startyear,12)
	$months = [Shift]::Left($now.Month,8)
	$days = [Shift]::Left($now.Day,3)
	$minfrac = [int](($now.Hour*60+$now.Minute)/205)
	return $years+$months+$days+$minfrac
}

function Generate-Assembly-Info
{
param(
	[string]$clsCompliant = "true",
	[string]$title, 
	[string]$description, 
	[string]$company, 
	[string]$product, 
	[string]$copyright, 
	[string]$version,
	[string]$file = $(throw "file is a required parameter.")
)
  $commit = Get-Git-Commit
  $asmInfo = "using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: CLSCompliantAttribute($clsCompliant )]
[assembly: ComVisibleAttribute(false)]
[assembly: AssemblyDescriptionAttribute(""$description"")]
[assembly: AssemblyProductAttribute(""$product / $commit"")]
[assembly: AssemblyCopyrightAttribute(""$copyright"")]
[assembly: AssemblyVersionAttribute(""$version"")]
[assembly: AssemblyInformationalVersionAttribute(""$version"")]
[assembly: AssemblyFileVersionAttribute(""$version"")]
[assembly: AssemblyDelaySignAttribute(false)]

"

	$dir = [System.IO.Path]::GetDirectoryName($file)
	if ([System.IO.Directory]::Exists($dir) -eq $false)
	{
		Write-Host "Creating directory $dir"
		[System.IO.Directory]::CreateDirectory($dir)
	}
	Write-Host "Generating assembly info file: $file"
	Write-Output $asmInfo > $file
}