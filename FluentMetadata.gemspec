version = File.read(File.expand_path("../VERSION",__FILE__)).strip  

Gem::Specification.new do |spec|
  spec.name        = 'fluentmetadata'
  spec.version     = version
  
  spec.has_rdoc = false
  spec.files = Dir["lib/FluentMetadata.*.dll", "lib/Readme.txt"]
  spec.files.reject! { |fn| fn.include? "Specs.dll"  }
  spec.description = 'FluentMetadata is describing Object-Metadata on one place, and using it from ASP.NET MVC 2 & 3, FluentNHibernate and EntityFramework 4 CodeFirst with EF Feature CTP 4'
  spec.summary     = 'FluentMetadata - Metadata on one place for .NET'
  
  spec.author			 = 'Albert Weinert'
  spec.email             = 'info@der-albert.com'
  spec.homepage          = 'http://wiki.github.com/DerAlbertCom/FluentMetadata/'
end
