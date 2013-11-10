var activeMenu = null;
function AppInterface(type) {
    this.iscrollClients = null;
    this.type = type;
    this.Scroller1 = null;
    this.fadeControl = "BodyBackgroundShadow";
    this.fadeControlSlider = "transparencySlider";


    this.AreaHeight = 0;
    this.backgroundOption = "Wallpaper";

    this.bgBodyImages = [
                            'images/bg/0.jpg',
                            'images/bg/1.jpg',
                            'images/bg/2.jpg',
                            'images/bg/3.jpg',
                            'images/bg/4.jpg'];

    this.bgBodyControl = "bdyHome";


    this.SplashControl = "welcomeText";
    this.UpdateSplashLogoPos = UpdateSplashLogoPosition;



    this.ScrollMetroMenu = null;

    /*
    this.MetroButtonImages = [
    { "ButtonSize": "Large", "ImageScr":  "images/Metro%20Icons/GreenButtonL.png" },
    { "ButtonSize": "Small", "ImageScr": "images/Metro%20Icons/BlueButtonS.png" },
    { "ButtonSize": "Small", "ImageScr":"images/Metro%20Icons/PurpleButtonS.png" },
    { "ButtonSize": "Large", "ImageScr": "images/Metro%20Icons/MaroonButtonL.png" },
    { "ButtonSize": "Small", "ImageScr": "images/Metro%20Icons/GreenButtonS.png" },
    { "ButtonSize": "Small", "ImageScr": "images/Metro%20Icons/OrangeButtonS.png" },
    { "ButtonSize": "Small", "ImageScr": "images/Metro%20Icons/BlueButtonS.png" },
    { "ButtonSize": "Small", "ImageScr": "images/Metro%20Icons/MaroonButtonS.png" }, 
    { "ButtonSize": "Large", "ImageScr": "images/Metro%20Icons/GreenButtonl.png" },
    { "ButtonSize": "Small", "ImageScr": "images/Metro%20Icons/BlueButtonS.png" }, 
    { "ButtonSize": "Small", "ImageScr":"images/Metro%20Icons/GreenButtonL.png" },	
    { "ButtonSize": "Small", "ImageScr": "images/Metro%20Icons/BlueButtonS.png" }, 	
    { "ButtonSize": "Small", "ImageScr":  "images/Metro%20Icons/GreenButtonL.png" },	
    { "ButtonSize": "Small", "ImageScr":  "images/Metro%20Icons/MaroonButtonS.png" },	
    { "ButtonSize": "Small", "ImageScr": "images/Metro%20Icons/MaroonButtonS.png" }, 
    { "ButtonSize": "Small", "ImageScr": "images/Metro%20Icons/OrangeButtonL.png" },
    ]
    */


    this.MetroButtonImagesMainMenu = [


   { "cmd": "list", "ButtonSize": "Small", "cmdPage": "Member/Tiles/PayRules/Main.aspx", "Desc": "Pay Rules", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/users.png" },
       { "cmd": "list", "ButtonSize": "Small", "cmdPage": "Member/Tiles/Employee/Main.aspx", "Desc": "Employees", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/users.png" },

    { "cmd": "list", "ButtonSize": "Small", "cmdPage": "Member/Tiles/CostCenters/Main.aspx", "Desc": "Cost Centers", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/users.png" },
    //{ "cmd": "new", "ButtonSize": "Small", "cmdPage": "Departments-List", "Desc": "departments", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/proposals.png" },
    //{ "cmd": "list", "ButtonSize": "Large", "cmdPage": "Positions-List", "Desc": "positions", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/proposals.png" },
    //{ "cmd": "list", "ButtonSize": "Large", "cmdPage": "Terminals-List", "Desc": "terminals", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/monitor.png" },
    //{ "cmd": "list", "ButtonSize": "Large", "cmdPage": "Clients-Detail", "Desc": "add new client", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/clients.png" },
      { "cmd": "list", "ButtonSize": "Small", "cmdPage": "Member/Tiles/Positions/Main.aspx", "Desc": "Positions", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/time.png" },
      { "cmd": "list", "ButtonSize": "Large", "cmdPage": "Member/Tiles/Supervisors/Main.aspx", "Desc": "Super visors", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/inventory.png" },
        { "cmd": "list", "ButtonSize": "Large", "cmdPage": "Member/Tiles/Departments/Main.aspx", "Desc": "Departments", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/inventory.png" },
        
    
    { "cmd": "list", "ButtonSize": "Small", "cmdPage": "Member/Tiles/TimeCard/Main.aspx", "Desc": "Time Card", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/users.png" },
   

   
    { "cmd": "list", "ButtonSize": "Small", "cmdPage": "Member/Tiles/ShiftPatterns/Main.aspx", "Desc": "Shift Patterns", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/time.png" },
   { "cmd": "list", "ButtonSize": "Small", "cmdPage": "0", "Desc": "Map", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/time.png" },

    ];

    
    this.MetroButtonImagesSubMenu = [



        { "cmd": "list", "ButtonSize": "Large", "cmdPage": "Associates-List", "Desc": "employees", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/users.png" },

    { "cmd": "list", "ButtonSize": "Small", "cmdPage": "Departments-List", "Desc": "departments", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/proposals.png" },
        { "cmd": "list", "ButtonSize": "Small", "cmdPage": "Positions-List", "Desc": "positions", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/clients.png" },


            { "cmd": "new", "ButtonSize": "Large", "cmdPage": "EventPayRolls", "Desc": "pay rolls", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/proposals.png" },


    { "cmd": "new", "ButtonSize": "Large", "cmdPage": "PayRules-List", "Desc": "pay rules", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/proposals.png" },

          { "cmd": "list", "ButtonSize": "Small", "cmdPage": "Timesheets-List", "Desc": "timesheets", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/proposals.png" },
{ "cmd": "new", "ButtonSize": "Small", "cmdPage": "Timesheets-List", "Desc": "reports", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/proposals.png" },


    //{ "cmd": "list", "ButtonSize": "Large", "cmdPage": "Terminals-List", "Desc": "terminals", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/monitor.png" },
               { "cmd": "list", "ButtonSize": "Large", "cmdPage": "Main", "Desc": "invaluable", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/shutdown.png" },
   // { "cmd": "list", "ButtonSize": "Small", "cmdPage": "Exit", "Desc": "exit", "ColorClass": "color_27 ", "ImageScr": "images/windows8_icons/shutdown.png" }

    ];


    this.MetroMenuControl = "";
    this.UpdateMetroMenuLayout = UpdateMetromenuLayout;


    this.UpdateAllControlsLayout = UpdateallControlsLayout;

    this.refreshScrollers = RefreshScrollers;

}

function UpdateMetromenuLayout(isSubmenu) {


    var RowCount = 0;
    var CreateNewCol = true;
    var InnerHtml = "";
    var ColGroupCount = 0;
    var SmallCount = 0;
    var MaxHeigh = 550;

    var MetroButtonImages;
    if (isSubmenu == true) {

        activeMenu = "submenu";
        if ((window.localStorage["SelectedSite"] === undefined) || (window.localStorage["SelectedSite"] == ""))
            $("#Logo").html("inTime");
        else {
            $("#Logo").html("inTime - " + window.localStorage["SelectedSiteName"]);

        }
        this.MetroButtonImages = this.MetroButtonImagesSubMenu;

        DownloadViewModel.DownloadSitePositions();

        DownloadViewModel.DownloadSiteDepartments();
    } else {
        $("#Logo").html("Invaluable");
        activeMenu = "mainmenu";
        this.MetroButtonImages = this.MetroButtonImagesMainMenu;
    }

    var TotalMeneButtons = this.MetroButtonImages.length;

    var pageSize = parent.getPageSize();

    var Width = $(window).width();
    var Height = $(window).height();

    var AreaHeight = Height - 100;

    this.AreaHeight = AreaHeight;
    var AreaAvailableRows = AreaHeight / 110;


    AreaAvailableRows = Math.floor(AreaAvailableRows);


    if (AreaAvailableRows > 5) {
        AreaAvailableRows = 5;
    }


    //ColorClass



    for (var i = 0; i < TotalMeneButtons; i < i++) {
        if (CreateNewCol == true) {
            InnerHtml += '<div class="SubMenuContant">';
            CreateNewCol = false;
            ColGroupCount++;
        }

        var cssClass = "MenuButtonSmall";

        var metroMenu_buttonClass = "metroMenu_button height_small width_small";
        var Desc = this.MetroButtonImages[i].Desc;
        var ColorClass = this.MetroButtonImages[i].ColorClass;
        var Cmd = this.MetroButtonImages[i].Cmd;
        var ImageScr = this.MetroButtonImages[i].ImageScr;
        var cmd = this.MetroButtonImages[i].cmd;
        var cmdPage = "'" + this.MetroButtonImages[i].cmdPage + "'";
        if (this.MetroButtonImages[i].ButtonSize == "Large") {
            //cssClass = "MenuButtonLarge";
            metroMenu_buttonClass = "metroMenu_button height_medium width_medium";
            SmallCount = 0;
            RowCount++;
        } else {
            SmallCount++;
            if (SmallCount == 2) {
                //cssClass = "MenuButtonSmall";
                metroMenu_buttonClass = "metroMenu_button height_small width_small";
                SmallCount = 0;
                RowCount++;
            }
        }


        //  InnerHtml += '<div class="' + cssClass + '" style="background-image: url(' + this.MetroButtonImages[i].ImageScr+ ')" ></div>';
        //images/icons/Clients.png

        ImageScr = "";
        InnerHtml += '<!-- metro span -->';
        InnerHtml += '<div class="' + metroMenu_buttonClass + '" cmd="' + cmd + '"  cmdPage="' + cmdPage + '"  onclick="openpage(' + cmdPage + ')"  >';
        InnerHtml += '<!-- metro button -->';
        InnerHtml += '<div class="metro_button  ' + ColorClass + '">';
        InnerHtml += '<div class="metro_button_content big_icon">  ';





        if ((cmdPage == "EventSiteselector") && (window.localStorage["SelectedSite"] != "")) {

            if (window.localStorage["SelectedSite"] === undefined)
                InnerHtml += '<div class="description">Administering [not selected]</div>';
            else {
                InnerHtml += '<div class="description">Administering [' + window.localStorage["SelectedSiteName"] + ']</div>';

                InnerHtml = InnerHtml.replace("color_15", "color_21");
            }

        } else
            InnerHtml += '<div class="description">' + Desc + '</div>';


        InnerHtml += '</div>';
        InnerHtml += '</div>';
        InnerHtml += '</div>';



        if (i + 1 == TotalMeneButtons) {
            InnerHtml += '</div>';
            break;
        }

        if (RowCount == AreaAvailableRows) {

            InnerHtml += '</div>';
            RowCount = 0;

            CreateNewCol = true;
        }

    }



    //$(".ui-content").css("width" , Width - 40 );
    $(".ui-content").css("width", "auto");
    $(".ui-content").css("width", $(".ui-content").css("width") - 60);

    // $("#ui-page ui-body-c , .ui-content").css("height", AreaHeight - 49);
    $("#ui-page ui-body-c , .ui-content").css("height", AreaHeight - 49);
    //$(".form-ui-container").css("height", AreaHeight - 49 - 15);
    $(".ui-MenuContant").css("height", AreaHeight - 65);


    //this.Scroller1 = new iScroll('containerSettings');







    $("#MenuContant").css("width", (ColGroupCount * 220) + 30 + 40);

    //new
    $("#MenuContant").css("height", AreaHeight);

    $("#MenuContant").html(InnerHtml);



    $("#MetroMenu").css("height", "auto");

    //$('#wrapper').css("width", Width   );



    //  $('#ScrollMetroMenu').css("width", Width - 40);
    //	if(myScroll == null)
    //	myScroll = new iScroll('ScrollMetroMenu');

    var pageSize = parent.getPageSize();
    $('#ScrollMetroMenu').css("width", pageSize.width);
    $('#ScrollMetroMenu').css("margin-left", "40px");
    if (this.ScrollMetroMenu == null) {

        this.ScrollMetroMenu = new iScroll('ScrollMetroMenu', { vScroll: false, hScrollbar: false, vScrollbar: false });
    } else {
        this.ScrollMetroMenu.refresh();
    }







    RefreshScrollers();




}



var iscrollClients2 = null;
function RefreshScrollers() {

}

function UpdateallControlsLayout() {
    this.UpdateMetroMenuLayout();
}

function UpdateSplashLogoPosition() {
    var WelcomeWidth = $(window).width() - 20;
    var Welcomeheight = WelcomeWidth * 0.411134;

    var TopPosition = $(window).height() - Welcomeheight / 0.411134;



    if ($(window).width() < 480)
        $('#' + this.SplashControl).css({ 'background': 'url("images/XFC2.png")', "background-repeat": "no-repeat", "background-position": "center", "background-size": "cover", "height": Welcomeheight + "px", "width": WelcomeWidth + "px", "position": "absolute", "top": TopPosition });

    else
        $('#' + this.SplashControl).css({ 'background': 'url("images/XFC2.png")', "background-repeat": "no-repeat", "background-position": "center", "background-size": "cover", "height": "192px", "width": "467px", "position": "absolute", "top": TopPosition });



}




