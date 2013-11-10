﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobiTme.Web.Functions.LocalStorage
{
    public static  class Countries
    {

        public static Dictionary<string, string> GetCountries()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();

            list.Add("4", "Afghanistan");
            list.Add("248", "Åland Islands");
            list.Add("8", "Albania");
            list.Add("12", "Algeria");
            list.Add("16", "American Samoa");
            list.Add("20", "Andorra");
            list.Add("24", "Angola");
            list.Add("660", "Anguilla");
            list.Add("10", "Antarctica");
            list.Add("28", "Antigua and Barbuda");
            list.Add("32", "Argentina");
            list.Add("51", "Armenia");
            list.Add("533", "Aruba");
            list.Add("36", "Australia");
            list.Add("40", "Austria");
            list.Add("31", "Azerbaijan");
            list.Add("44", "Bahamas");
            list.Add("48", "Bahrain");
            list.Add("50", "Bangladesh");
            list.Add("52", "Barbados");
            list.Add("112", "Belarus");
            list.Add("56", "Belgium");
            list.Add("84", "Belize");
            list.Add("204", "Benin");
            list.Add("60", "Bermuda");
            list.Add("64", "Bhutan");
            list.Add("68", "Bolivia Plurinational State of");
            list.Add("535", "Bonaire Sint Eustatius and Saba");
            list.Add("70", "Bosnia and Herzegovina");
            list.Add("72", "Botswana");
            list.Add("74", "Bouvet Island");
            list.Add("76", "Brazil");
            list.Add("86", "British Indian Ocean Territory");
            list.Add("96", "Brunei Darussalam");
            list.Add("100", "Bulgaria");
            list.Add("854", "Burkina Faso");
            list.Add("108", "Burundi");
            list.Add("116", "Cambodia");
            list.Add("120", "Cameroon");
            list.Add("124", "Canada");
            list.Add("132", "Cape Verde");
            list.Add("136", "Cayman Islands");
            list.Add("140", "Central African Republic");
            list.Add("148", "Chad");
            list.Add("152", "Chile");
            list.Add("156", "China");
            list.Add("162", "Christmas Island");
            list.Add("166", "Cocos (Keeling) Islands");
            list.Add("170", "Colombia");
            list.Add("174", "Comoros");
            list.Add("178", "Congo");
            list.Add("180", "Congo the Democratic Republic of the");
            list.Add("184", "Cook Islands");
            list.Add("188", "Costa Rica");
            list.Add("384", "Côte d'Ivoire");
            list.Add("191", "Croatia");
            list.Add("192", "Cuba");
            list.Add("531", "Curaçao");
            list.Add("196", "Cyprus");
            list.Add("203", "Czech Republic");
            list.Add("208", "Denmark");
            list.Add("262", "Djibouti");
            list.Add("212", "Dominica");
            list.Add("214", "Dominican Republic");
            list.Add("218", "Ecuador");
            list.Add("818", "Egypt");
            list.Add("222", "El Salvador");
            list.Add("226", "Equatorial Guinea");
            list.Add("232", "Eritrea");
            list.Add("233", "Estonia");
            list.Add("231", "Ethiopia");
            list.Add("238", "Falkland Islands (Malvinas)");
            list.Add("234", "Faroe Islands");
            list.Add("242", "Fiji");
            list.Add("246", "Finland");
            list.Add("250", "France");
            list.Add("254", "French Guiana");
            list.Add("258", "French Polynesia");
            list.Add("260", "French Southern Territories");
            list.Add("266", "Gabon");
            list.Add("270", "Gambia");
            list.Add("268", "Georgia");
            list.Add("276", "Germany");
            list.Add("288", "Ghana");
            list.Add("292", "Gibraltar");
            list.Add("300", "Greece");
            list.Add("304", "Greenland");
            list.Add("308", "Grenada");
            list.Add("312", "Guadeloupe");
            list.Add("316", "Guam");
            list.Add("320", "Guatemala");
            list.Add("831", "Guernsey");
            list.Add("324", "Guinea");
            list.Add("624", "Guinea-Bissau");
            list.Add("328", "Guyana");
            list.Add("332", "Haiti");
            list.Add("334", "Heard Island and McDonald Islands");
            list.Add("336", "Holy See (Vatican City State)");
            list.Add("340", "Honduras");
            list.Add("344", "Hong Kong");
            list.Add("348", "Hungary");
            list.Add("352", "Iceland");
            list.Add("356", "India");
            list.Add("360", "Indonesia");
            list.Add("364", "Iran Islamic Republic of");
            list.Add("368", "Iraq");
            list.Add("372", "Ireland");
            list.Add("833", "Isle of Man");
            list.Add("376", "Israel");
            list.Add("380", "Italy");
            list.Add("388", "Jamaica");
            list.Add("392", "Japan");
            list.Add("832", "Jersey");
            list.Add("400", "Jordan");
            list.Add("398", "Kazakhstan");
            list.Add("404", "Kenya");
            list.Add("296", "Kiribati");
            list.Add("408", "Korea Democratic People's Republic of");
            list.Add("410", "Korea Republic of");
            list.Add("414", "Kuwait");
            list.Add("417", "Kyrgyzstan");
            list.Add("418", "Lao People's Democratic Republic");
            list.Add("428", "Latvia");
            list.Add("422", "Lebanon");
            list.Add("426", "Lesotho");
            list.Add("430", "Liberia");
            list.Add("434", "Libya");
            list.Add("438", "Liechtenstein");
            list.Add("440", "Lithuania");
            list.Add("442", "Luxembourg");
            list.Add("446", "Macao");
            list.Add("807", "Macedonia the former Yugoslav Republic of");
            list.Add("450", "Madagascar");
            list.Add("454", "Malawi");
            list.Add("458", "Malaysia");
            list.Add("462", "Maldives");
            list.Add("466", "Mali");
            list.Add("470", "Malta");
            list.Add("584", "Marshall Islands");
            list.Add("474", "Martinique");
            list.Add("478", "Mauritania");
            list.Add("480", "Mauritius");
            list.Add("175", "Mayotte");
            list.Add("484", "Mexico");
            list.Add("583", "Micronesia Federated States of");
            list.Add("498", "Moldova Republic of");
            list.Add("492", "Monaco");
            list.Add("496", "Mongolia");
            list.Add("499", "Montenegro");
            list.Add("500", "Montserrat");
            list.Add("504", "Morocco");
            list.Add("508", "Mozambique");
            list.Add("104", "Myanmar");
            list.Add("516", "Namibia");
            list.Add("520", "Nauru");
            list.Add("524", "Nepal");
            list.Add("528", "Netherlands");
            list.Add("540", "New Caledonia");
            list.Add("554", "New Zealand");
            list.Add("558", "Nicaragua");
            list.Add("562", "Niger");
            list.Add("566", "Nigeria");
            list.Add("570", "Niue");
            list.Add("574", "Norfolk Island");
            list.Add("580", "Northern Mariana Islands");
            list.Add("578", "Norway");
            list.Add("512", "Oman");
            list.Add("586", "Pakistan");
            list.Add("585", "Palau");
            list.Add("275", "Palestinian Territory Occupied");
            list.Add("591", "Panama");
            list.Add("598", "Papua New Guinea");
            list.Add("600", "Paraguay");
            list.Add("604", "Peru");
            list.Add("608", "Philippines");
            list.Add("612", "Pitcairn");
            list.Add("616", "Poland");
            list.Add("620", "Portugal");
            list.Add("630", "Puerto Rico");
            list.Add("634", "Qatar");
            list.Add("638", "Réunion");
            list.Add("642", "Romania");
            list.Add("643", "Russian Federation");
            list.Add("646", "Rwanda");
            list.Add("652", "Saint Barthélemy");
            list.Add("654", "Saint Helena Ascension and Tristan da Cunha");
            list.Add("659", "Saint Kitts and Nevis");
            list.Add("662", "Saint Lucia");
            list.Add("663", "Saint Martin (French part)");
            list.Add("666", "Saint Pierre and Miquelon");
            list.Add("670", "Saint Vincent and the Grenadines");
            list.Add("882", "Samoa");
            list.Add("674", "San Marino");
            list.Add("678", "Sao Tome and Principe");
            list.Add("682", "Saudi Arabia");
            list.Add("686", "Senegal");
            list.Add("688", "Serbia");
            list.Add("690", "Seychelles");
            list.Add("694", "Sierra Leone");
            list.Add("702", "Singapore");
            list.Add("534", "Sint Maarten (Dutch part)");
            list.Add("703", "Slovakia");
            list.Add("705", "Slovenia");
            list.Add("90", "Solomon Islands");
            list.Add("706", "Somalia");
            list.Add("710", "South Africa");
            list.Add("239", "South Georgia and the South Sandwich Islands");
            list.Add("728", "South Sudan");
            list.Add("724", "Spain");
            list.Add("144", "Sri Lanka");
            list.Add("729", "Sudan");
            list.Add("740", "Suriname");
            list.Add("744", "Svalbard and Jan Mayen");
            list.Add("748", "Swaziland");
            list.Add("752", "Sweden");
            list.Add("756", "Switzerland");
            list.Add("760", "Syrian Arab Republic");
            list.Add("158", "Taiwan Province of China");
            list.Add("762", "Tajikistan");
            list.Add("834", "Tanzania United Republic of");
            list.Add("764", "Thailand");
            list.Add("626", "Timor-Leste");
            list.Add("768", "Togo");
            list.Add("772", "Tokelau");
            list.Add("776", "Tonga");
            list.Add("780", "Trinidad and Tobago");
            list.Add("788", "Tunisia");
            list.Add("792", "Turkey");
            list.Add("795", "Turkmenistan");
            list.Add("796", "Turks and Caicos Islands");
            list.Add("798", "Tuvalu");
            list.Add("800", "Uganda");
            list.Add("804", "Ukraine");
            list.Add("784", "United Arab Emirates");
            list.Add("826", "United Kingdom");
            list.Add("840", "United States");
            list.Add("581", "United States Minor Outlying Islands");
            list.Add("858", "Uruguay");
            list.Add("860", "Uzbekistan");
            list.Add("548", "Vanuatu");
            list.Add("862", "Venezuela Bolivarian Republic of");
            list.Add("704", "Viet Nam");
            list.Add("92", "Virgin Islands British");
            list.Add("850", "Virgin Islands U.S.");
            list.Add("876", "Wallis and Futuna");
            list.Add("732", "Western Sahara");
            list.Add("887", "Yemen");
            list.Add("894", "Zambia");
            list.Add("716", "Zimbabwe");
            return list;
        }
    }
}