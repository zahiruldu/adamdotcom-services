<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" />
  <xsl:template match="/">
    <xsl:apply-templates select="//text()"/>
  </xsl:template>

  <xsl:template match="text()">"<xsl:copy-of select="normalize-space(.)"/>"<xsl:if test="not(position()=last())">,</xsl:if></xsl:template>
</xsl:stylesheet>