
Include ".\Tools\psake\psake_ext.ps1"

properties { 
  $base_dir  = resolve-path .
  $revision =  Generate-Revision(2010)
  $lib_dir = "$base_dir\external"
  $build_dir = "$base_dir\build" 
  $buildartifacts_dir = "$build_dir\" 
  $sln_file = "$base_dir\Source\FluentMetadata.sln" 
  $version = "0.5.1.$revision"
  $tools_dir = "$base_dir\Tools"
  $release_dir = "$base_dir\Release"
} 

task default -depends Test

task Clean { 
  remove-item -force -recurse $buildartifacts_dir -ErrorAction SilentlyContinue 
  remove-item -force -recurse $release_dir -ErrorAction SilentlyContinue 
} 

task Init -depends Clean { 

    Generate-Assembly-Info `
        -file "$base_dir\Source\GlobalAssemblyInfo.cs" `
        -title "FluentMetadata $version" `
        -description "A Metadata Framework for ASP.MVC, FluentNHibernate and Entity Framework CodeFirst" `
        -product "FluentMetadata $version" `
        -version $version `
        -clsCompliant "false" `
        -copyright "Copyright © Albert Weinert 2010"
} 

task Compile -depends Init { 
  new-item $buildartifacts_dir -itemType directory 
  exec { msbuild /t:Rebuild /verbosity:minimal "/p:OutDir=$buildartifacts_dir" "/p:Platform=Any CPU" "/p:Configuration=Release" "$sln_file" }
  copy-item readme.txt $build_dir\readme.txt
} 

task Test40 -depends Compile {
  exec { & $tools_dir\xUnit\xunit.console.clr4.exe $build_dir\FluentMetadata.EntityFramework.Specs.dll }
  exec { & $tools_dir\xUnit\xunit.console.clr4.exe $build_dir\FluentMetadata.Core.Specs.dll }
  exec { & $tools_dir\xUnit\xunit.console.clr4.exe $build_dir\FluentMetadata.MVC.Specs.dll }  
}

task Test -depends Test40

task CleanGem -ContinueOnError {	
   Remove-Item .\*.gem
   exec { gem uninstall fluentmetadata -a -x }	
}

task Gem -depends CleanGem, Compile {
   $version | out-file .\VERSION -encoding ASCII
   
   exec { gem build .\FluentMetadata.gemspec }
   exec { gem install fluentmetadata }
}

task Release -depends Test, Gem {
    new-item $release_dir -itemType directory 
	
    exec {
    
      & $tools_dir\Zip\zip.exe -9 -A -j `
        $release_dir\FluentMetadata.$version.zip `
        $build_dir\readme.txt `
        $build_dir\FluentMetadata.Core.dll `
        $build_dir\FluentMetadata.MVC.dll `
        $build_dir\FluentMetadata.FluentNHibernate.dll `
        $build_dir\FluentMetadata.EntityFramework.dll 
    }
}

task Publish -depends Release {
	exec {  & $tools_dir\WinSCP\winscp.com /command  `
			 "open aweinert@s200.aperea.com"  `
			 "cd /wikiupload/projects/fluentmetadata/"   `
			 "put $release_dir\FluentMetadata.$version.zip"  `
			 "exit" 
		}
}
