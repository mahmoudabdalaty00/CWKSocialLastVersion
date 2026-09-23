using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace Application.Service.Helpers;

public class SamlResponse
{
       public static string GenerateSamlResponse(
            string entityId, string acsUrl, string userEmail,
            string firstName,string lastName,string fullname, 
            string audience, string pfxPath, string pfxPassword, string? relaystate = null)
        {
            try
            {
                var now = DateTime.UtcNow;
                var responseId = "_" + Guid.NewGuid();
                var assertionId = "_" + Guid.NewGuid();

                // Create SAML Response root
                var samlResponse = new XmlDocument();
                var response = samlResponse.CreateElement("samlp:Response", "urn:oasis:names:tc:SAML:2.0:protocol");
                response.SetAttribute("ID", responseId);
                response.SetAttribute("Version", "2.0");
                response.SetAttribute("IssueInstant", now.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                response.SetAttribute("Destination", acsUrl);

                // Add Issuer
                var issuer = samlResponse.CreateElement("saml:Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
                issuer.InnerText = entityId;
                response.AppendChild(issuer);

                // Add Status
                var status = samlResponse.CreateElement("samlp:Status", "urn:oasis:names:tc:SAML:2.0:protocol");
                var statusCode = samlResponse.CreateElement("samlp:StatusCode", "urn:oasis:names:tc:SAML:2.0:protocol");
                statusCode.SetAttribute("Value", "urn:oasis:names:tc:SAML:2.0:status:Success");
                status.AppendChild(statusCode);
                response.AppendChild(status);

                // Create Assertion
                var assertion = samlResponse.CreateElement("saml:Assertion", "urn:oasis:names:tc:SAML:2.0:assertion");
                assertion.SetAttribute("Version", "2.0");
                assertion.SetAttribute("ID", assertionId);
                assertion.SetAttribute("IssueInstant", now.ToString("yyyy-MM-ddTHH:mm:ssZ"));

                // Add Issuer to Assertion
                var assertionIssuer =
                    samlResponse.CreateElement("saml:Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
                assertionIssuer.InnerText = entityId;
                assertion.AppendChild(assertionIssuer);

                // Add Subject
                var subject = samlResponse.CreateElement("saml:Subject", "urn:oasis:names:tc:SAML:2.0:assertion");
                var nameId = samlResponse.CreateElement("saml:NameID", "urn:oasis:names:tc:SAML:2.0:assertion");
                nameId.InnerText = userEmail;
                subject.AppendChild(nameId);

                var subjectConfirmation = samlResponse.CreateElement("saml:SubjectConfirmation",
                    "urn:oasis:names:tc:SAML:2.0:assertion");
                subjectConfirmation.SetAttribute("Method", "urn:oasis:names:tc:SAML:2.0:cm:bearer");

                var subjectConfirmationData = samlResponse.CreateElement("saml:SubjectConfirmationData",
                    "urn:oasis:names:tc:SAML:2.0:assertion");
                subjectConfirmationData.SetAttribute("NotOnOrAfter",
                    now.AddMinutes(5).ToString("yyyy-MM-ddTHH:mm:ssZ"));
                subjectConfirmationData.SetAttribute("Recipient", acsUrl);
                subjectConfirmation.AppendChild(subjectConfirmationData);

                subject.AppendChild(subjectConfirmation);
                assertion.AppendChild(subject);

                // Add Conditions
                var conditions = samlResponse.CreateElement("saml:Conditions", "urn:oasis:names:tc:SAML:2.0:assertion");
                conditions.SetAttribute("NotBefore", now.AddMinutes(-5).ToString("yyyy-MM-ddTHH:mm:ssZ"));
                conditions.SetAttribute("NotOnOrAfter", now.AddMinutes(5).ToString("yyyy-MM-ddTHH:mm:ssZ"));

                var audienceRestriction = samlResponse.CreateElement("saml:AudienceRestriction",
                    "urn:oasis:names:tc:SAML:2.0:assertion");
                var audienceElement =
                    samlResponse.CreateElement("saml:Audience", "urn:oasis:names:tc:SAML:2.0:assertion");
                audienceElement.InnerText = audience;
                audienceRestriction.AppendChild(audienceElement);
                conditions.AppendChild(audienceRestriction);
                assertion.AppendChild(conditions);

                // Add AuthnStatement
                var authnStatement =
                    samlResponse.CreateElement("saml:AuthnStatement", "urn:oasis:names:tc:SAML:2.0:assertion");
                authnStatement.SetAttribute("AuthnInstant", now.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                authnStatement.SetAttribute("SessionIndex", assertionId);

                var authnContext =
                    samlResponse.CreateElement("saml:AuthnContext", "urn:oasis:names:tc:SAML:2.0:assertion");
                var authnContextClassRef = samlResponse.CreateElement("saml:AuthnContextClassRef",
                    "urn:oasis:names:tc:SAML:2.0:assertion");
                authnContextClassRef.InnerText = "urn:oasis:names:tc:SAML:2.0:ac:classes:unspecified";
                authnContext.AppendChild(authnContextClassRef);
                authnStatement.AppendChild(authnContext);
                assertion.AppendChild(authnStatement);

                // Add AttributeStatement with user attributes
                var attributeStatement =
                    samlResponse.CreateElement("saml:AttributeStatement", "urn:oasis:names:tc:SAML:2.0:assertion");

                attributeStatement.AppendChild(CreateAttribute(samlResponse, "emailaddress", userEmail));
                attributeStatement.AppendChild(CreateAttribute(samlResponse, "givenname", firstName));
                attributeStatement.AppendChild(CreateAttribute(samlResponse, "surname", lastName));
                attributeStatement.AppendChild(CreateAttribute(samlResponse, "nameidentifier", userEmail));
                attributeStatement.AppendChild(CreateAttribute(samlResponse, "name", fullname));
                attributeStatement.AppendChild(CreateAttribute(samlResponse, "email", userEmail));

                assertion.AppendChild(attributeStatement);
                response.AppendChild(assertion);
                samlResponse.AppendChild(response);

                // Load the .pfx certificate and sign the assertion
                SignXml(assertion, pfxPath, pfxPassword);

                return $@"
                    <html>
                    <body onload='document.forms[0].submit()'>
                        <form action='{acsUrl}' method='post'>
                            <input type='hidden' name='RelayState' value='{relaystate}' />
                            <input type='hidden' name='SAMLResponse' value='{Convert.ToBase64String(Encoding.UTF8.GetBytes(samlResponse.OuterXml))}' />
                        </form>
                    </body>
                    </html>";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static XmlElement CreateAttribute(XmlDocument doc, string name, string value)
        {
            try
            {
                var attribute = doc.CreateElement("saml:Attribute", "urn:oasis:names:tc:SAML:2.0:assertion");
                attribute.SetAttribute("Name", name);

                var attributeValue = doc.CreateElement("saml:AttributeValue", "urn:oasis:names:tc:SAML:2.0:assertion");
                attributeValue.InnerText = value;

                attribute.AppendChild(attributeValue);
                return attribute;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void SignXml(XmlElement elementToSign, string pfxPath, string pfxPassword)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2(pfxPath, pfxPassword,
                    X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

                SignedXml signedXml = new SignedXml(elementToSign);
                signedXml.SigningKey = cert.GetRSAPrivateKey();

                Reference reference = new Reference();
                reference.Uri = "#" + elementToSign.GetAttribute("ID");

                reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
                reference.AddTransform(new XmlDsigExcC14NTransform());
                signedXml.AddReference(reference);

                KeyInfo keyInfo = new KeyInfo();
                keyInfo.AddClause(new KeyInfoX509Data(cert));
                signedXml.KeyInfo = keyInfo;

                signedXml.ComputeSignature();
                XmlElement xmlDigitalSignature = signedXml.GetXml();

                elementToSign.AppendChild(elementToSign.OwnerDocument.ImportNode(xmlDigitalSignature, true));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
}