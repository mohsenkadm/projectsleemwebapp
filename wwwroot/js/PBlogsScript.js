const baseUrl = "/";
function call_ajax_json(method, url, param, object, call_back_func) {
    var userToken = getCookie("token1");
    mouseevent("progress");
    $.ajax({
      method: method,
      url: baseUrl + url + param,
      cache: true, async: true,
      dataType: 'json', 
      contentType: 'application/json; charset=UTF-8',
      data: JSON.stringify(object),
      headers: {
        'Authorization': `Bearer ${userToken}`,
      },
        success: (respons) => {
           
            mouseevent("default");
            if (respons.success) {
                if (typeof (call_back_func) == 'function') {
                    if (respons.data != undefined)
                        call_back_func(respons.data);
                    else
                        call_back_func();
                }
                else if (typeof (call_back_func) == 'string' && call_back_func == 'return') {
                    return respons.data;
                }
                if (respons.msg != null && respons.msg != undefined)
                    toust.success(respons.msg);
            }
            else {
                if (respons.msg != null && respons.msg != undefined)
                    toust.error(respons.msg);
            }
        },
        error: (e) => {
            mouseevent("default");
            toust.error('حدث خطأ عند الأتصال');
        }
    });
  }
function call_ajax(method, url, object, call_back_func) {
    var userToken = getCookie("token1");
    mouseevent("progress");
    $('.preloader').fadeIn();
      $.ajax({
      method: method,
      url: baseUrl + url ,
      cache: true, async: true,
          data: object,
      headers: {
        'Authorization': `Bearer ${userToken}`,
      },
          success: (respons) => {
         //   /  debugger;
              mouseevent("default");
              $('.preloader').fadeOut();
              if (respons.success) {
                  if (typeof (call_back_func) == 'function') {
                      if (respons.data != undefined)
                          call_back_func(respons.data);
                      else
                          call_back_func();
                  }
                  else if (typeof (call_back_func) == 'string'
                      && call_back_func == 'return') {
                      return respons.data;
                  }
                  if (respons.msg != null && respons.msg != undefined)
                      toust.success(respons.msg);
              }
              else {
                  if (respons.msg != null && respons.msg != undefined)
                      toust.error(respons.msg);
              }

          },
          error: (e) => {
              mouseevent("default");
              $('.preloader').fadeOut();
             toust.error('حدث خطأ عند الأتصال');
          }
      });
}
function call_Action(url,i) {
    //debugger;
    if (i === 1) {
        $('#notmodel').modal('hide');
    }
   else  if (i === 2) {
        $('#commentmodel').modal('hide');
    }
    else if (i === 3) {
        $('#likemodel').modal('hide');
    }
    var userToken = getCookie("token1");
        mouseevent("progress");
        $.ajax({
            method: 'GET',
            url: baseUrl + url,
            cache: true, async: true,
            headers: {
                'Authorization': `Bearer ${userToken}`,
            },
            success: (respons) => {
                $('.preloader').fadeIn();
                mouseevent("default");
                var from = respons.indexOf("<!-- CUT FROM HERE -->");
                document.body.innerHTML = respons.substring(from, respons.length - 16);
                window.scrollTo(0, 0);
                var mydiv = document.getElementById("mydiv");
                var scripts = mydiv.getElementsByTagName("script");
               
                for (var i = 0; i < scripts.length; i++) {
                   
                    eval(scripts[i].innerText);
                }
                $('.preloader').fadeOut();
            }
        });

}
function mouseevent(type) {
    $("body").css("cursor", type);
    //type =progress ,default
}
function setCookie(cname, cvalue, exdays) {
    //debugger;
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24  *60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function getCookie(cname) {
  //  debugger;
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
var id1;
function fillpost(data) {
    if (data.length === 0) {
        toust.error("No Information");
        return;
    }
    $.each(data, function (i, item) {

        var str2 = item.item_date;
        var res2 = str2.slice(0, 10);
        var rows1 = " <article class='wd   wow fadeInUp' data-wow-duration='500ms' data-wow-delay='400ms'>" +
            "<div  class='post-block text-right' style='border-radius: 15px;'>" +
            " <div class='content' >" +
            " <div class='price-title' style='padding:0px;' >" +
            "<h2><a > " + item.item_name + ": اسم المنتج</a></h2>" +
            " <br/>" +
            " <h3>" + item.item_price + ": سعر المنتج</h3>" +
            " <h3>" + res2 + " : تاريخ اخر عرض  للمنتج</h3>" +
            " <h3>" + item.item_dateils + " : تفاصيل المنتج</h3>" +
            " <h3 id='lastpriceid" + item.post_id + "'>" + item.lastprice + " :اخر سعر تم مزايدة عليه</h3>" +
            " <h3 id='lastuserid" + item.post_id + "'>" + item.username + " :تمت اخر مزايدة من قبل المستخدم</h3>" +
            " <div class='filtr-item '>" +
            " <div class='portfolio-block' style='margin-bottom:0;'>" +
            "   <img  src='images/imag_post/" + item.image + "' style='width:100%; height:30%;' alt='image'>" +
            "     <div class='caption'>" +
            " <a class='search-icon image-popup' onclick =\"setimageinsidemodel('" + item.image + "')\" data-toggle='modal' data-target='#image'  data-effect='mfp-with-zoom' data-scroll>" +
            "   <i class='tf-ion-android-search'></i>" +
            "</a>" +
            " <h4>تكبير الصورة</h4>" +
            " </div>" +
            " </div>" +
            "</div>" +
            "<div class='divdown'>" +
            "<button type='button' class='btn btn-transparent' data-toggle='modal' data-target='#postusermodel' onclick='getpostuser(" + item.post_id + ")'  >عرض المستخدمين</button>" +
            "</div>"+
        " </div>" +
            "</div>" +
            "</div>" +
            " </article>";
        $('#posts').append(rows1);
    });
}
function getpostuser(id) {
    var o = {
        post_id: id,
    };
    id1 = id
    call_ajax("GET", "posts/getpostuser", o, setdataforpostuser);
};

function setdataforpostuser(data) {
    $('#postuser').empty();
    if (data.length === 0) {
        toust.error("لا توجد مزايدات");
    }
    $.each(data, function (i, item) {

        var str2 = item.data_insert;
        var res2 = str2.slice(0, 10);
        var rows = "<div class='price-title text-right'>" +
            " <h3><a > " + item.username + " :اسم المستخدم</a></h3>" +
            "   <span style='float:none;'> " + res2 + "/ تاريخ المزايدة  </span>" +
            "</div>" + "<div class='price-title' style='padding:0px;' >" +
            " <h3 style='padding: 1%;background-color:#292F36;' class=' text-right'>" + item.price_item + " :السعر </h3>"
            + "</div>";
        $('#postuser').append(rows);
    });
    var row = "<div class='clearfix'>" +
        "<div class='col-lg-9 col-md-9 col-sm-9 col-xs-8' >" +
        " <div class='form-group'>" +
        "<input placeholder='اكتب السعر للمزايدة على هذا المنشور' type='number' class='bo form-control text-right' autocomplete='off' name='textforprice' id='textforprice' />" +
        " </div>" +
        "</div >" +
        "<div class='col-lg-2 col-md-2 col-sm-2 col-xs-2'>" +
        "<button type='button'  name='commendbtn' id='commendbtn' onclick='setpostuser()'  class='btn btn-transparent' >  مزايدة </button> <br/>" +
        " </div>"
        "</div >";
    $('#postuser').append(row);
};

function setpostuser() {
    var o = {
        price_item: $("#textforprice").val(),
        post_id: id1,
    };
    
    if (o.price_item === "" || o.price_item.trim() === "") {
        toust.error("يرجى كتابة السعر");
        return;
    }
    call_ajax("Get", "posts/setpostuser", o, setdataforpost);
};
function setdataforpost(data) {
  
    document.getElementById('lastpriceid' + data.post_id).innerText = data.price_item +':اخر سعر تم مزايدة عليه';
    document.getElementById('lastuserid' + data.post_id).innerText = data.username +':تمت اخر مزايدة من قبل المستخدم';
    getpostuser(id1);
}
var state = {
    'querySet': null, //
    'page': 1,
    'rows': 5,
    'window': 7,
} 
function pagination(querySet, page, rows) {
    var trimStart = (page - 1) * rows
    var trimEnd = trimStart + rows
    var trimmedData = querySet.slice(trimStart, trimEnd);
    var pages = Math.round(querySet.length / rows);
    return {
        'querySet': trimmedData,
        'pages': pages,
    }
}
function pageButtons(pages, i) {
    var wrapper = document.getElementById('pagination-wrapper')
    wrapper.innerHTML = ``
    console.log('Pages:', pages)
    var maxLeft = (state.page - Math.floor(state.window / 2))
    var maxRight = (state.page + Math.floor(state.window / 2))
    if (maxLeft < 1) {
        maxLeft = 1
        maxRight = state.window
    }
    if (maxRight > pages) {
        maxLeft = pages - (state.window - 1)
        if (maxLeft < 1) {
            maxLeft = 1
        }
        maxRight = pages
    }
    for (var page = maxLeft; page <= maxRight; page++) {
        wrapper.innerHTML += `<li> <button  value=${page} class="page ">${page}</button> </li>`
    }
    if (state.page != 1) {
        wrapper.innerHTML = `<li> <button value=${1} class="page ">&#171; </button> </li>` + wrapper.innerHTML
    }
    if (state.page != pages) {
        wrapper.innerHTML += `<li> <button value=${page} class="page "> &#187;</button> </li>`
    }

    $('.page').on('click', function () {
        $('#posts').empty()
        state.page = Number($(this).val())
        var object = {
            item_name: '',
        };
        call_ajax("GET", "Posts/Getpost", object, buildTable);
       
    });

}
function buildTable(data) {
    //state.page = 1;
    state.querySet = data;
    var dat = pagination(state.querySet, state.page, state.rows);
    fillpost(dat.querySet);
    pageButtons(dat.pages, 1);
}
function logut() {
    call_ajax("Get", "Account/Logout", null, afterlogout);
};
function afterlogout() {
    document.cookie = "token1= ; expires = Thu, 01 Jan 1970 00:00:00 GMT";
    call_Action("home/index")
  
}
function setimage(src) {
    $("#setimage2").attr("src", src);
}
function setimageinsidemodel(image) {
    $('#setimage1').empty();
    var rows1 = "<img class='mySlides'  src='images/imag_post/" + image + "' style='width:100%; display: block;'>";
    $('#setimage1').append(rows1);
};
function forgat_pass() {
    $("#login").hide();
    $("#email").fadeIn();
}
function after_write_email() {
    $("#email").hide();
    $("#confirm").fadeIn();
}
function after_confirm() {
    $("#confirm").hide();
    $("#newpass").fadeIn();
}
function after_updata_pass() {
    $("#newpass").hide();
    $("#login").fadeIn();
}

