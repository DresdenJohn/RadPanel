/* select form */
var gszCategoryID;

function clearPop () {
	$("#popupCon").hide();
	$("#popupFailed").hide();
	$("#popupComplete").hide();
	$("#popupError").hide();
	$(".overlayShop").hide();

}

function DisplayItemListAsync(nPageNo,szCategoryID,nGenderFlag,szItemName)
{
    szItemName = encodeURIComponent(szItemName); 

    $.ajax({
        async: false, //false : synchronize, true : asynchronize
        type: "GET",
        url: "GetItemList.php?categoryid="+szCategoryID+"&currpage="+nPageNo+"&gender="+nGenderFlag+"&itemname="+szItemName,
        cache: false,
        data: {},
        beforeSend: function(){
			//setListInfoDiv();
           // $("#error").hide();    
            //$("#item_list_content").hide();
        },
        complete: function(){
           //$("#mainsub_recent").show();  
		   	$(".itemlist").show()
			$(".filluplist").hide()
        },
        error: function(xmlHttp, textStatus, errorThrown){
           // $("#errormessage").html(xmlHttp.responseText);
           // $("#error").show();                          
        },
        success: function(result){
            $(".listBox").html(result); 

            if ($(".innerBox").find(".first").find(".btn_buy").attr("href"))
            {
                nItemID = $(".innerBox").find(".first").find(".btn_buy").attr("href").split("#")[1];
                DisplayItemInfoAsync( nItemID,1); 
            }
             $(".inputBox").find(".selectMenu").html( $(".innerBox h2").html());
            //$("#item_list_content").show(); 
        }
    });
}

function DisplayItemInfoAsync(nItemID,nSendGiftFlag) //nSendGiftFlag -> 1: Just Buy , 2: Gift
{

	    $.ajax({
        async: false, //false : synchronize, true : asynchronize
        type: "GET",
        url: "FlyffItemInfo.php?sendgiftflag="+ nSendGiftFlag+"&itemid="+nItemID,
        cache: false,
        data: {},
        beforeSend: function(){
           // $("#error").hide();    
            //$("#item_list_content").hide();
        },
        complete: function(){
           //$("#mainsub_recent").show();  
		   	$(".itemlist").show()
            $(".selectForm .list").hide();
			$(".filluplist").hide()
        },
        error: function(xmlHttp, textStatus, errorThrown){
           // $("#errormessage").html(xmlHttp.responseText);
           // $("#error").show();                          
        },
        success: function(result){ 
            $(".infoBox").html(result); 
            //$("#item_list_content").show(); 
        }
    });
}


function DisplayPurchaseConfrimPop(nItemID,nGiftFlag,strFriendInfo) 
{

	$("#friendinfo").val(strFriendInfo);
	$("#puritemid").val(nItemID);
	$("#giftflag").val(nGiftFlag);
	$(".overlayShop").show();
	$("#popupCon").show();
	
}

function DisplayErrorPop() 
{
	$(".overlayShop").show();
	$("#popupError").show();

}

function ChargeFlyff(nItemID,nSendGiftFlag,strFriendInfo) //nSendGiftFlag -> 1: Just Buy , 2: Gift
{
    $.ajax({
        async: true, //false : synchronize, true : asynchronize
        type: "GET",
        url: "ChargeFlyffItem.php?itemid="+ nItemID+"&presentflag="+ nSendGiftFlag+"&f_character="+strFriendInfo,
        cache: false,
        data: {},
        beforeSend: function(){
            //$("#error").hide();
            //$("#item_list_content").hide();
        },
        complete: function(){
           //$("#mainsub_recent").show();
		   DisplayItemInfoAsync(nItemID,1);
        },
        error: function(xmlHttp, textStatus, errorThrown){
           // $("#errormessage").html(xmlHttp.responseText);
           // $("#error").show();
        },
        success: function(result){
			if( result == 1 )
			{
				$(".overlayShop").show();
				$("#popupComplete").show();
			}
			else
			{
				$(".overlayShop").show();
				$("#popupFailed").show();
			}
        }
    });
}


$(document).ready(function() {
    $(".listBox").html($("#notice").html());
    $("#genderSelect .list").hide();
	DisplayItemInfoAsync("",1);

	$('#categoryid li a').click(function(){
        var szCategoryID = $(this).attr('href').split('#')[1];
        gszCategoryID = szCategoryID;
        $('#genderSelect .selected').html($("#both").html());
        $('#itemSearch').val("");
		DisplayItemListAsync(1,szCategoryID,"","");
    });

	$('#genderSelect a').live('click',function() {
		var nGender = $(this).attr("href").split('#')[1];
        $("#genderflag").val(nGender);
        if(gszCategoryID != null)
        {
		    DisplayItemListAsync(1,gszCategoryID,nGender,"");
        }
    });

	$(".selectForm").height("18px");
    $(".selectForm .list").hide();
	$(".selectForm .selected").live('click',function(){
		$(this).siblings(".list").show();
		 var formWidth = $(this).width()+18;
		$(this).siblings(".list").width(formWidth);

	});

	$(".selectForm .list a").live('click',function(){
        var value = $(this).html();
        $(this).parent(".list").siblings(".selected").html(value);
		$(this).parent(".list").hide();
	});

	$(".selectForm").live('mouseleave',function(){
		$(this).children(".list").hide();
	});

	$('#footer ul li').click(function(){
		var footerID = $(this).attr("id");
        
        switch(footerID) {
            case "liRepOut" : showPopPurchaseHistory(1); break;
            case "liNotice" : $(".itemlist").show(); $(".listBox").html($("#notice").html());break;
            default: break;
        }
    });

    $('#divRepOutList a').live('click',function() {
       var page = $(this).attr("page");
       showPopPurchaseHistory(page);
    });

    $('#searchROL').live('click',function() {
       showPopPurchaseHistory(1);
    });

	$('#imgiteminfo a').live('click',function() { //-- itemlist imgbtn(send iteminfo)
        var itemid = $(this).attr("href").split('#')[1];
		$("#friendinfo").val("");
		 DisplayItemInfoAsync(itemid,1);
    });

	$('.btn_gift').live('click',function() { //-- itemlist giftbtn(send iteminfo)
        var itemid = $(this).attr("href").split('#')[1];
		$("#friendinfo").val("");
		 DisplayItemInfoAsync(itemid,2);
    });

	$('#friendselect a').live('click',function() { //--itemdetail select friend
		var value = $(this).attr("href").split('#')[1];
		$("#friendinfo").val(value);
    });

	$('#giftbtnArea a').live('click',function() { //--itemdetail giftbtn
		
		var strFriendInfo = $("#friendinfo").val();

		
		if (strFriendInfo == "")
		{
			DisplayErrorPop();
		}
		else
		{
			var itemid = $(this).attr("href").split('#')[1];
			DisplayPurchaseConfrimPop(itemid,2,strFriendInfo);
		}
	
	
    });

	$('.btn_buy').live('click',function() { //-- itemlist  buybtn
        var itemid = $(this).attr("href").split('#')[1];
        
		DisplayPurchaseConfrimPop(itemid,1,"");
    });

	$('.btn_detailBuy').live('click',function() { //-- itemdetail buybtn
        var itemid = $(this).attr("href").split('#')[1];
        $(".overlayShop").show();
		DisplayPurchaseConfrimPop(itemid,1,"");
    });

	
	$('.btn_yes').live('click',function() { //-- confirmPop btn
        $(".overlayShop").show();
        $("#popupCon").hide();
        var nGiftFlag = $("#giftflag").val();
        var nPurItemID = $("#puritemid").val();
        var szFriendInfo = $("#friendinfo").val();
		ChargeFlyff(nPurItemID,nGiftFlag,szFriendInfo);
    });


	$('.search a').live('click',function() {
		var strItemName = $("#itemSearch").val();
		DisplayItemListAsync(1,"",$("#genderflag").val(),strItemName);
    });


	$(".btn_no").live('click',function() {
		clearPop ();			
        $(".overlayShop").hide();
	});	

	$(".btn_popupclose_B").live('click',function() {
		clearPop ();			
        $(".overlayShop").hide();
	});	

	$(".btn_popupclose a").live('click',function() {
		clearPop ();			
        $(".overlayShop").hide();
	});	
	
	$('.btn_login').live('click',function() {
        var strUsername = $("#login_user").val();
		var strPassword = $("#login_pass").val();
		var strCharacter = $("#login_char").val();
		ShopLogin( strUsername, strPassword, strCharacter );
    });
})

function showPopFillupHistory(nPageNo) {	


        var year  = $("#year").html()
        var month = $("#month").html()
        var day1  = $("#day1").children(".selected").html()
        var day2  = $("#day2").children(".selected").html()
        if (year == null) year = ""
        if (month == null) month = ""
        if (day1 == null) day1 = ""
        if (day2 == null) day2 = ""

    $.ajax({
        async: false, //false : synchronize, true : asynchronize
        type: "GET",
        url: "FlyffRepInList.asp",
        cache: false,
        data: "currpage="+nPageNo+"&year="+year+"&month="+month+"&day1="+day1+"&day2="+day2,
        beforeSend: function(){
           // $("#error").hide();                               
           //$(".overlayList").show();
        },
        complete: function(){
           //$("#mainsub_recent").show();            
           $(".itemlist").hide()
           $(".filluplist").show()
           $(".selectForm .list").hide();
           $(".overlayList").hide();
        },
        error: function(xmlHttp, textStatus, errorThrown){
            $("#errormessage").html(xmlHttp.responseText);
           // $("#error").show();                          
        },
        success: function(result){ 
			$(".filluplist").html("");
            $(".filluplist").html(result);            
        }
    });
}
function showPopPurchaseHistory(nPageNo) {	
		$(".itemlist").hide()
		$(".filluplist").show()

        var year  = $("#year").html()
        var month = $("#month").html()
        var day1  = $("#day1").html()
        var day2  = $("#day2").html()
        if (year == null) year = ""
        if (month == null) month = ""
        if (day1 == null) day1 = ""
        if (day2 == null) day2 = ""
		
    $.ajax({
        async: false, //false : synchronize, true : asynchronize
        type: "GET",
        url: "FlyffRepOutList.php",
        cache: false,
        data: "currpage="+nPageNo+"&year="+year+"&month="+month+"&day1="+day1+"&day2="+day2,
        beforeSend: function(){
           // $("#error").hide();                    
        },
        complete: function(){
           //$("#mainsub_recent").show();            
           $(".itemlist").hide()
           $(".filluplist").show()
           $(".selectForm .list").hide();
        },
        error: function(xmlHttp, textStatus, errorThrown){
           // $("#errormessage").html(xmlHttp.responseText);
           // $("#error").show();   
        },
        success: function(result){ 			
            $(".filluplist").html("");
            $(".filluplist").html(result);            
        }
    });
}