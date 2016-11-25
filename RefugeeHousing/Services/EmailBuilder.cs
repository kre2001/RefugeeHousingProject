﻿using System.Collections.Generic;
using RefugeeHousing.Models;
using RefugeeHousing.ViewModels;
using SendGrid.Helpers.Mail;

namespace RefugeeHousing.Services
{
    public interface IEmailBuilder
    {
        Mail Build(PropertyEnquiry enquiry, ApplicationUser owner);
    }

    public class EmailBuilder : IEmailBuilder
    {
        private const string FromAddress = "refugeehousingproject@example.com";

        public Mail Build(PropertyEnquiry enquiry, ApplicationUser owner)
        {
            var from = new Email(FromAddress);
            var to = new Email(owner.Email);
            var subject = "Email from Refugee Housing Project";
            var content = new Content("text/plain", ContentText(enquiry));

            var email = new Mail(@from, subject, to, content) {ReplyTo = new Email(enquiry.EnquirerEmail)};

            return email;
        }

        private static string ContentText(PropertyEnquiry enquiry)
        {
            var languagesSpoken = GetLanguagesSpoken(enquiry);

            // Do not add indents to trailing lines of this string. It causes SendGrid to format them
            // differently to the first line.
            return $@"Hello,

You have a query regarding your property from {enquiry.EnquirerName}, on behalf of the organization '{enquiry.OrganizationName}'.

For more details on the organization, visit {enquiry.OrganizationWebsite}

Languages spoken by {enquiry.EnquirerName}: {languagesSpoken}

This person has expressed their willingness to sign medium-to-long-term leases on behalf of refugee families, and to pay first and last month's rent up front.";
        }

        private static string GetLanguagesSpoken(PropertyEnquiry enquiry)
        {
            var languages = new List<string>();
            if (enquiry.EnquirerSpeaksEnglish)
            {
                languages.Add("English");
            }
            if (enquiry.EnquirerSpeaksGreek)
            {
                languages.Add("Greek");
            }

            return string.Join(", ", languages);
        }
    }
}