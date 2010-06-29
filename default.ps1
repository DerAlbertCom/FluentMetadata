properties { 
  $base_dir  = resolve-path .
  $lib_dir = "$base_dir\external"
  $build_dir = "$base_dir\Build" 
  $buildartifacts_dir = "$build_dir\" 
  $sln_file = "$base_dir\Source\FluentMetadata.sln" 
  $version = "0.2.1"
  $tools_dir = "$base_dir\Tools"
  $release_dir = "$base_dir\Release"
} 

task default -depends Release

task Clean { 
  remove-item -force -recurse $buildartifacts_dir -ErrorAction SilentlyContinue 
  remove-item -force -recurse $release_dir -ErrorAction SilentlyContinue 
} 

task Init -depends Clean { 
    . .\psake_ext.ps1
    
    Generate-Assembly-Info `
        -file "$base_dir\Source\GlobalAssemblyInfo.cs" `
        -title "FluentMetadata $version" `
        -description "A Metadata Framework for ASP.MVC and FluentNHibernate" `
        -product "FluentMetadata $version" `
        -version $version `
        -clsCompliant "false" `
        -copyright "Copyright © Albert Weinert 2010"
        
    new-item $release_dir -itemType directory 
    new-item $buildartifacts_dir -itemType directory 
} 

task CopyExternals
{
   exec { Copy-Item $lib_dir\xUnit\*.* $buildartifacts_dir }
}
task Compile -depends Init { 
  exec { msbuild "/p:OutDir=$buildartifacts_dir" "/p:Platform=Any CPU" "$sln_file" }
} 

task Test -depends Compile  {
  exec { & $tools_dir\xUnit\xunit.console.exe $build_dir\FluentMetadata.Core.Specs.dll }
  exec { & $tools_dir\xUnit\xunit.console.exe $build_dir\FluentMetadata.MVC.Specs.dll }
}


task Docu -depends Test {
   exec { & $lib_dir\xUnit\ReportGenerator.exe /generator:HTML /path:$build_dir /assembly:$build_dir\FluentMetadata.Core.Specs.dll /assembly:$build_dir\FluentMetadata.MVC.Specs.dll  }
}

task Release -depends Docu {
    
    exec {
    
      & $tools_dir\Zip\zip.exe -9 -A -j `
        $release_dir\FluentMetadata.$version.zip `
        $build_dir\FluentMetadata.Core.dll `
        $build_dir\FluentMetadata.MVC.dll `
        $build_dir\FluentMetadata.FluentNHibernate.dll 
    }
}