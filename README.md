# XMLDiff_Readable_Diffgram
Replaces XML element names in a diffgram with their corresponding element names taken from a source XML file. Requires an input source file (pre-modified xml file) and a diffgram generated with Microsoft's [XMLDiff tool](https://docs.microsoft.com/en-us/previous-versions/dotnet/articles/aa302294(v=msdn.10)?redirectedfrom=MSDN).

Input Source:
```
<PartPriceInfo xmlns:ns1="http://www.Subaru.com">
   <ns1:Subaru model="Legacy">
      <ns1:Muffler> 400 </ns1:Muffler>
      <ns1:Bumper> 100 </ns1:Bumper>
      <ns1:Floormat> 50 </ns1:Floormat>
      <ns1:WindShieldWipers> 20 </ns1:WindShieldWipers>
   </ns1:Subaru>
   <ns1:Subaru model="Outback">
      <ns1:Muffler> 500 </ns1:Muffler>
      <ns1:Bumper> 150 </ns1:Bumper>
      <ns1:Floormat> 75 </ns1:Floormat>
      <ns1:WindShieldWipers> 20 </ns1:WindShieldWipers>
   </ns1:Subaru>
</PartPriceInfo>
```

Input Diffgram:
```
<?xml version="1.0" encoding="utf-16" ?> 
<xd:xmldiff version="1.0" srcDocHash="2079810781567709607" 
options="IgnoreChildOrder IgnoreNamespaces IgnorePrefixes" 
xmlns:xd="https://schemas.microsoft.com/xmltools/2002/xmldiff">
   <xd:node match="1">
      <xd:add type="1" name="Subaru" ns="http://www.Subaru.com" prefix="ns2">
         <xd:add type="2" name="model">Impreza</xd:add> 
            <xd:add>
               <ns2:Muffler xmlns:ns2="http://www.Subaru.com">450</ns2:Muffler> 
               <ns2:Bumper xmlns:ns2="http://www.Subaru.com">120</ns2:Bumper> 
               <ns2:Floormat xmlns:ns2="http://www.Subaru.com">65</ns2:Floormat> 
            </xd:add>
         <xd:add match="/1/2/4" opid="1" /> 
      </xd:add>
      <xd:node match="2">
         <xd:node match="1">
            <xd:change match="1">600</xd:change> 
         </xd:node>
         <xd:add>
            <ns2:WindShieldWipers xmlns:ns2="http://www.Subaru.com">25</ns2:WindShieldWipers>
         </xd:add>
         <xd:remove match="4" opid="1" /> 
      </xd:node>
   </xd:node>
   <xd:descriptor opid="1" type="move" /> 
</xd:xmldiff> 
```

Example Output:
```
<xd:xmldiff version="1.0" srcDocHash="2079810781567709607" options="IgnoreChildOrder IgnoreNamespaces IgnorePrefixes" xmlns:xd="https://schemas.microsoft.com/xmltools/2002/xmldiff">
  <PartPriceInfo xmlns:ns1="http://www.Subaru.com">
    <added-element name="Subaru" ns="http://www.Subaru.com" prefix="ns2">
      <added-attribute name="model">Impreza</added-attribute>
      <xd:add>
        <ns2:Muffler xmlns:ns2="http://www.Subaru.com">450</ns2:Muffler>
        <ns2:Bumper xmlns:ns2="http://www.Subaru.com">120</ns2:Bumper>
        <ns2:Floormat xmlns:ns2="http://www.Subaru.com">65</ns2:Floormat>
      </xd:add>
      <xd:add match="/1/2/4" opid="1" />
    </added-element>
    <ns1:Subaru model="Outback">
      <ns1:Muffler>
        <xd:change match="1">600</xd:change>
      </ns1:Muffler>
      <xd:add>
        <ns2:WindShieldWipers xmlns:ns2="http://www.Subaru.com">25</ns2:WindShieldWipers>
      </xd:add>
      <Removed opid="1">
        <ns1:WindShieldWipers> 20 </ns1:WindShieldWipers>
      </Removed>
    </ns1:Subaru>
  </PartPriceInfo>
  <xd:descriptor opid="1" type="move" />
</xd:xmldiff>
```
