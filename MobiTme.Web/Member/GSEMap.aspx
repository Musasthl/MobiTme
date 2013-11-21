<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GSEMap.aspx.cs" Inherits="MobiTme.Member.GSEMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-2.0.3.min.js"></script>

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <style type="text/css">
        html, body, form {
            padding: 0;
            margin: 0;
        }

        .btn-nav {
            color: #FFFFFF;
            display: inline-block;
            line-height: 30px;
            font-size: 13px;
            font-weight: bold;
            cursor: pointer;
            text-decoration: none;
            margin-top: 4px;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            fixMapLayout();
            //init();
            $('#btnMainMenu').click(function (e) {
                parent.slideTo('Menu', false);
            });

            $('#btnLogout').click(function (e) {
                // call logout page method
                var confirmLogout = confirm("Are you sure you want to log out?");
                if (confirmLogout == true) {
                    parent.slideTo('Login', true);
                }
            });
        });


        function fixMapLayout() {
            var width = $(document).width();
            var height = $(document).height();

            var parentSize = parent.getPageSize();

            $('#map3d').css('width', parentSize.width);
            $('#map3d').css('height', parentSize.height - 40);
        }

        //var ge;
        //google.load("earth", "1");
        //google.load("earth", "1");

        //var ge = null;

        //function init() {
        //    initialize();
        //    google.earth.createInstance("map3d", initCallback, failureCallback);

        //    var myLatlng = new google.maps.LatLng(-25.363882, 131.044922);

        //    var marker = new google.maps.Marker({
        //        position: myLatlng,
        //        map: map,
        //        title: 'Hello World!'
        //    });
        //}


        ////////function initialize() {
        ////////    var myLatlng = new google.maps.LatLng(-25.363882, 131.044922);
        ////////    var mapOptions = {
        ////////        zoom: 4,
        ////////        center: myLatlng,
        ////////        mapTypeId: google.maps.MapTypeId.ROADMAP
        ////////    }
        ////////    var map = new google.maps.Map(document.getElementById('map3d'), mapOptions);

        ////////    var marker = new google.maps.Marker({
        ////////        position: myLatlng,
        ////////        map: map,
        ////////        title: 'Hello World!'
        ////////    });
        ////////}

        ////////google.maps.event.addDomListener(window, 'load', initialize);
        var ge;
        google.load("earth", "1", { "other_params": "sensor=false" });

        function init() {
            google.earth.createInstance('map3d', initCB, failureCB);
        }

        var lastLat = null;
        var lastLon = null;
        function initCB(instance) {

            ge = instance;
            ge.getWindow().setVisibility(true);
            ge.getNavigationControl().setVisibility(ge.VISIBILITY_HIDE);
            ge.getLayerRoot().enableLayerById(ge.LAYER_ROADS, true);


            //// Add the placemark to Earth.
            //ge.getFeatures().appendChild(getMarker());

            //// Add the placemark to Earth.
            //ge.getFeatures().appendChild(getMarker1());


            ListUserSites();


            if (lastLat != null) {
                // Move the camera.
                var la = ge.createLookAt('');
                la.set(lastLat, lastLon, 0, ge.ALTITUDE_RELATIVE_TO_GROUND, -8.541, 1.213, 4000000);
                ge.getView().setAbstractView(la);
            }
        }

        function failureCB(errorCode) {
        }

        function getMarker(sideid, name, iconcolour, lat, lon) {
            // Create the placemark.
            var placemark = ge.createPlacemark('');
            placemark.setName(name);

            // Set the placemark's location.  
            var point = ge.createPoint('');
            point.setLatitude(parseFloat(lat));
            point.setLongitude(parseFloat(lon));
            placemark.setGeometry(point);


            // Apply stylemap to a placemark.
            placemark.setStyleSelector(getStyleMap(iconcolour));
            lastLat = parseFloat(lat);
            lastLon = parseFloat(lon);

            makerEventHandler(sideid, placemark);
            return placemark;
        }
         
        function makerEventHandler(sideid, placemark) {
            // listen to the click event
            google.earth.addEventListener(placemark, 'click', function (event) {
                var text = 'Click:';

                function addToMessage(append1, append2) {
                    text += ' ' + append1 + ': ' + append2 + '\n';
                }

                //addToMessage('target type', event.getTarget().getType());
                //addToMessage('currentTarget type',
                //             event.getCurrentTarget().getType());
                //addToMessage('button', event.getButton());
                //addToMessage('clientX', event.getClientX());
                //addToMessage('clientY', event.getClientY());
                //addToMessage('screenX', event.getScreenX());
                //addToMessage('screenY', event.getScreenY());
                //addToMessage('latitude', event.getLatitude());
                //addToMessage('longitude', event.getLongitude());
                //addToMessage('altitude', event.getAltitude());
                //addToMessage('didHitGlobe', event.getDidHitGlobe());
                //addToMessage('altKey', event.getAltKey());
                //addToMessage('ctrlKey', event.getCtrlKey());
                //addToMessage('shiftKey', event.getShiftKey());
                //addToMessage('timeStamp', event.getTimeStamp());

                // Prevent default balloon from popping up for marker placemarks
                event.preventDefault();

                // wrap alerts in API callbacks and event handlers
                // in a setTimeout to prevent deadlock in some browsers
                setTimeout(function () {
                    // alert(text);
                    parent.slideTo('Menu', false, sideid);
                }, 0);
            });
        }
        function getStyleMap(iconcolour) {
            //http://maps.google.com/mapfiles/ms/icons/blue-dot.png
            //    enter image description herehttp://maps.google.com/mapfiles/ms/icons/red-dot.png
            //    enter image description herehttp://maps.google.com/mapfiles/ms/icons/purple-dot.png
            //    enter image description herehttp://maps.google.com/mapfiles/ms/icons/yellow-dot.png
            //    enter image description herehttp://maps.google.com/mapfiles/ms/icons/green-dot.png
            // Create a style map.
            var styleMap = ge.createStyleMap('');
            iconcolour = 'green'
            // Create normal style for style map.
            var normalStyle = ge.createStyle('');
            var normalIcon = ge.createIcon('');
            normalIcon.setHref('http://maps.google.com/mapfiles/ms/icons/' + iconcolour.toLowerCase() + '-dot.png');
            normalStyle.getIconStyle().setIcon(normalIcon);

            // Create highlight style for style map.
            var highlightStyle = ge.createStyle('');
            var highlightIcon = ge.createIcon('');
            highlightIcon.setHref('http://maps.google.com/mapfiles/ms/icons/' + iconcolour.toLowerCase() + '-dot.png');
            highlightStyle.getIconStyle().setIcon(highlightIcon);
            highlightStyle.getIconStyle().setScale(5.0);

            styleMap.setNormalStyle(normalStyle);
            styleMap.setHighlightStyle(highlightStyle);
            return styleMap;
        }


        function ListUserSites() {
            $.ajax({
                type: "Post",
                async: false,
                url: window.location.pathname + "/ListUserSites",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: "{}",
                success: function (response) {

                    var jsonData = JSON.parse(response.d);
                    if (jsonData != null) {
                        for (var r = 0; r < jsonData.length; r++) {
                            var row = jsonData[r];
                       
                            // Add the placemark to Earth.
                            ge.getFeatures().appendChild(getMarker(row.SiteID, row.Site, row.MapIconColour, row.GGPS01, row.GGPS02));
                             
                        }

       
                    }
                },
                error: function (error) {
                    // report error back to user
                    alert(error.responseText);
                }
            });
        }

        google.setOnLoadCallback(init);



    </script>
</head>
<body onload="">

    <div style="background-color: #252525; height: 40px;">
        <span id="btnLogout" class="btn-nav" value="Logout" style="float: left; margin-left: 20px;"><i class="icon-white icon-user"></i>Logout</span>


        <span id="btnMainMenu" class="btn-nav" value="Main Menu" style="float: right; margin-right: 20px;">
            <i class="icon-white icon-th-large"></i>Main Menu
        </span>


    </div>



    <div id="map3d"></div>


</body>
</html>
