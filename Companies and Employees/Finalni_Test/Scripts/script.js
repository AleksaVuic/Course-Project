$(document).ready(function () {


    var host = "http://" + window.location.host + "/";
    var registerEndPoint = "api/Account/Register";
    var loginEndPoint = "Token";
    var zaposleniEndPoint = "api/zaposleni";
    var jediniceEndPoint = "api/jedinice";

    var token = null;
    var headers = {};


     loadAll();
     $("#header").show();
     $("#dataDiv").show();
    // $("#").show();

     $("#showRegister").click(function () {
         $("#header").hide();
         $("#registerDiv").show();
     });
     $("#registerCancel").click(function (e) {
         RefreshCredentials();
         e.preventDefault();
         $("#header").show();
         $("#registerDiv").hide();
     });
     $("#showLogin").click(function () {
         $("#header").hide();
         $("#loginDiv").show();
     });
     $("#loginCancel").click(function (e) {
         e.preventDefault();
         RefreshCredentials();
         $("#header").show();
         $("#loginDiv").hide();
     });
     $("#logoutBtn").click(function () {
         token = null;
         headers = {};
         $("#createDiv").hide();
         $("#searchDiv").hide();
         $("#logoutDiv").hide();
         $("#header").show();
         loadAll();
     });
    //  ============================================================== 
    // FORMS

   
     $("#registerForm").submit(function (e) {
      
         e.preventDefault();
         var email = $("#registerEmail").val();
         var psw = $("#registerPsw").val();
        

         var sendData ={
             "Email": email,
             "Password": psw,
             "ConfirmPassword": psw
         }

         $.ajax({
             type: "POST",
             url: host + registerEndPoint,
             data: sendData

         }).done(function(data,status){
             RefreshCredentials();
             $("#registerDiv").hide();
             $("#loginDiv").show();


         }).fail(function(data, status){
             alert("Greska prilikom registracije!");
         });

     });
     $("#loginForm").submit(function (e) {
         e.preventDefault();

         var email = $("#loginEmail").val();
         var psw = $("#loginPsw").val();
         var sendData = {
             "grant_type": "password",
             "username": email,
             "password": psw
         };

         $.ajax({
             type: "POST",
             url: host + "Token",
             data: sendData
         })
             .done(function (data, status) {
                 token = data.access_token;
                 RefreshCredentials();
                 $("#loginDiv").hide();
                 $("#logoutDiv").show();
                 $("#searchDiv").show(); 
                 $("#createDiv").show();
                 $("#loggedUser").text("Prijavljeni korisnik: " + data.userName);
                 loadAll();

             })
             .fail(function (data, status) {
                 alert("Greska prilikom prijave!");
             });
         

     });
     $("#searchForm").submit(function(e){
         e.preventDefault();
         if (token) {
             headers.Authorization = "Bearer " + token;
         }
         var least = +$("#least").val();
         var most = +$("#most").val();
         var sendData = {
             "Najmanje": least,
             "Najvise": most
         };

         $.ajax({
             type: "POST",
             url: host + "api/pretraga",
             data: sendData,
             headers: headers
         })
             .done(function (data, status) {
                 $("#least").val("");
                 $("#most").val("");
                 fillData(data, status);
             })
             .fail(function (data, status) {
                 alert("Greska prilikom pretrage!");
             });
        

     });
     $("#createForm").submit(function (e) {
         e.preventDefault();
         if (token) {
             headers.Authorization = "Bearer " + token;
         }

         var role = $("#role").val();
         var name = $("#name").val();
         var yearbirth = +$("#yearbirth").val();
         var yearemployed = +$("#yearemployed").val();
         var salary = +$("#salary").val();
         var id = $("#select").val();

         var sendData = {
             "ImeIPrezime": name,
             "Rola": role,
             "GodinaRodjenja": yearbirth,
             "GodinaZaposlenja": yearemployed,
             "Plata": salary,
             "JedinicaId": id
         };

         $.ajax({
             type: "POST",
             url: host + zaposleniEndPoint,
             headers: headers,
             data: sendData
         }).done(function (data, status) {
             RefreshTable();
         }).fail(function () {
             alert("Greska prilikom dodavanja!");
         });

     });

     $("body").on("click", "#deleteBtn", function () {
         var id = this.name;
         if (token) {
             headers.Authorization = "Bearer " + token;
         }

         if(token){
             headers.Authorization = "Bearer " + token;
         }

         $.ajax({
             type: "DELETE",
             url: host + "api/zaposleni/" + id.toString(),
             headers: headers
         })
         .done(function(data, status){
             RefreshTable();
         })
         .fail(function(data, status){
             alert("Greska prilikom brisanja!");
         });

     });
    


    // =================================================================
    // | LOAD & FILL

    function loadAll() {

        loadZaposleni();
        loadDropdown();

    }
    function loadZaposleni() {

        var requestUrl = host + zaposleniEndPoint;
        $.getJSON(requestUrl, fillData);

    }
    function loadDropdown() {

        var dropDownUrl = host + jediniceEndPoint;
        $.getJSON(dropDownUrl, fillDropDown);

    }
    function fillData(data, status) {

        var $data = $("#dataDiv");
        $data.empty();
        var header = $("<h2>Zaposleni</h2>");

        if (status === "success") {
            $data.append(header);
            var table = $("<table border=4 class=\"table table-bordered table-hover\"></table>");
            var th = "";
            
            if (token) {

                th = $("<th>Ime i prezime</th> <th>Rola</th> <th>Godina zaposlenja</th> <th>Godina rodjenja</th> <th>Jedinica</th> <th>Plata</th> <th>Akcija</th> ");
            }
            else
            {
                th = $("<th>Ime i prezime</th> <th>Rola</th> <th>Godina zaposlenja</th> <th>Jedinica</th> ");
            }
            th.css("background-color", "yellow");
            th.css("text-align", "center");

            table.append(th);

            for (var i = 0; i < data.length; i++) {

                var td = "<td>";
                var tdend = "</td>";
                var row = "<tr>";

                if (token)
                {
                    var deleteBtn = "<td><button id=deleteBtn name=" + data[i].Id + ">Delete</button></td>";
                    row += td + data[i].ImeIPrezime + tdend +
                        td + data[i].Rola + tdend +
                        td + data[i].GodinaZaposlenja + tdend +
                        td + data[i].GodinaRodjenja + tdend +
                        td + data[i].Jedinica.Ime + tdend +
                        td + data[i].Plata + tdend + deleteBtn;
                        
                }
               else
               {
                   row += td + data[i].ImeIPrezime + tdend +
                       td + data[i].Rola + tdend +
                       td + data[i].GodinaZaposlenja + tdend +
                        td + data[i].Jedinica.Ime + tdend;

               }

                    row += "</tr>";
                table.append(row);
            }

            $data.append(table);
        }
        else {
            header = $("<h2> Greska prilikom ucitavanja </h2>")
            $data.append(header);
        }

    }
    function fillDropDown(data, status) {

        var select = $("#select");
        select.empty();

        if (status === "success") {

            for (var i = 0; i < data.length; i++) {

                 var option = "<option value=" + data[i].Id + ">" + data[i].Ime + "</option>";

                select.append(option);
            }

        } else {
            alert("Doslo je do greske prilikom ucitavanja jedinica");
        }
    }




    // ========================================================================
    // | REFRESH |

    function RefreshCredentials() {
        $("#registerEmail").val("");
        $("#registerPsw").val("");
        $("#registerPswConfirm").val("");
        $("#loginEmail").val("");
        $("#loginPsw").val("");
    }
    function RefreshTable() {
        $("#role").val("");
        $("#name").val("");
        $("#yearbirth").val("");
        $("#yearemployed").val("");
        $("#salary").val("");
        loadAll();
    }

}); 