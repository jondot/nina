<configuration>
    <configSections>
        <section name="nhaml" type="NHaml.Configuration.NHamlConfigurationSection, NHaml"/>
    </configSections>

    <nhaml autoRecompile="false" templateCompiler="CSharp3" encodeHtml="false" useTabs="false" indentSize="2">
        <assemblies>
            <clear/>
            <add assembly="System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089"/>
            <add assembly="System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089"/>
        </assemblies>
        <namespaces>
            <clear/>
            <add namespace="System.Collections"/>
            <add namespace="System.Linq"/>
        </namespaces>
    </nhaml>
</configuration>