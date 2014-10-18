<?php
	require_once( 'include/config.php' );
	
	$content = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
				<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="ko" lang="ko">
				<head>
					<title>'.$site_title.'</title>
					<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
					<meta http-equiv="X-UA-Compatible" content="IE=8" />
					<link rel="stylesheet" type="text/css" href="css/ingame.css"/>
					<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
					<script type="text/javascript" src="js/ingame.js"></script>
				</head>
				<body>
					<input type= "hidden" id="friendinfo" value="">
					<input type= "hidden" id="puritemid" value="">
					<input type= "hidden" id="giftflag" value="">
					<input type= "hidden" id="gategoryid" value="">
					<input type= "hidden" id="searchgender" value="">
					<input type= "hidden" id="genderflag" value="3">
					<input type= "hidden" id="retcode" value="">

					<div id="wrap">
					<div class="overlayShop" style="display:none"></div>
						<div id="header">
							<h1 class="blind" onclick="location.href=\'index.php\'" style="cursor:pointer">Flyff</h1>
							<a href="#" class="btn_close">'.$lang['close'].'</a>
							<div class="selectArea">
								<div class="selectRightbg">
									<div class="inputBox" style="width:167px">
										<span id="genderSelect" class="selectForm"> 
											<span  class="selected" style="width:40px">'.$lang['both'].'</span>
											<span class="list"  style="display:none;" >
												<a href="#3" id="both">'.$lang['both'].'</a>
												<a href="#1">'.$lang['smale'].'</a>
												<a href="#2">'.$lang['sfemale'].'</a>
											</span>
										</span>
										<span class="selectMenu"></span>
									</div>
								</div>
							</div>';
							//<div class="search">
							//	<input type="text" id="itemSearch" name="" value=""  /><a href="#" class="btn_search blind">'.$lang['search2'].'</a>
							//</div>
			$content .= '</div>
						<div id="container">
							<div class="shopMenu">
								<div class="innerBox">
									<ul id="categoryid">';
										$keys = array_keys( $shop_cat );
										for( $i = 0; $i < sizeof( $keys ); $i++ )
										{
											if( $i == 0 )
												$content .= '<li class="first"><a href="#AAAAAAAA'.$keys[$i].'"><span>'.$shop_cat[$keys[$i]].'</span></a></li>';
											else if( $i == sizeof( $keys ) )
												$content .= '<li class="last"></li>';
											else
												$content .= '<li><a href="#AAAAAAAA'.$keys[$i].'"><span>'.$shop_cat[$keys[$i]].'</span></a></li>';
										}
						$content .= '</ul>
								</div>
							</div>
							<div class="content">
								<div class="itemlist">
									<div class="listBox"></div>
									<div class="infoBox"></div>
								</div>
								<div class="filluplist" style="display:none;"></div>
							</div>
						</div>
						<div id="footer">
							<ul>
								<li class="first" id="liRepOut"><a href="#"><span>'.$lang['paysum'].'</span></a></li>
								<li class="center" id="liRepOut"><a href="#"><span>'.$lang['pursum'].'</span></a></li>
								<li class="last" id="liNotice"><a href="'.$charge_url.'" target="_blank">'.$lang['charge'].'</a></li>
							</ul>
						</div>
					</div>
					<div id="popupCon" style="display:none">
						<div id="popupArea" type="Confirm" >
							<div class="btn_popupclose"><a href="#">'.$lang['close'].'</a></div>
							<div class="popupText_box line2">
								<div class="popupTitle">'.$lang['confirm'].'</div>
								<div class="popupText">'.$lang['earn'].'</div>
							</div>
							<div class="btn_popup">
								<div class="buttonBox">
									<a href="#" class="btn_yes">'.$lang['yes'].'</a> &nbsp;<a href="#" class="btn_no">'.$lang['no'].'</a>
								</div>
							</div>
						</div>
					</div>
					<div id="popupComplete" style="display:none">
						<div id="popupArea" type="PurchaseComplete" >
							<div class="btn_popupclose"><a href="#">'.$lang['close'].'</a></div>
							<div class="popupText_box line3">
								<div class="popupTitle">'.$lang['confirm'].'</div>
								<div class="popupText">'.$lang['pursucc'].'</div>
							</div>
							<div class="btn_popup">
								<div class="buttonBox">
									<a href="#" class="btn_popupclose_B">'.$lang['close'].'</a></a>
								</div>
							</div>
						</div>
					</div>
					<div id="popupFailed" style="display:none">
						<div id="popupArea" type="PurchaseFailed" >
							<div class="btn_popupclose"><a href="#">'.$lang['close'].'</a></div>
							<div class="popupText_box line2">
								<div class="popupTitle">'.$lang['confirm'].'</div>
								<div class="popupText">'.$lang['failed'].'</div>
							</div>
							<div class="btn_popup">
								<div class="buttonBox">
									<a href="#" class="btn_popupclose_B">'.$lang['close'].'</a></a>
								</div>
							</div>
						</div>
					</div>
					<div id="popupError" style="display:none">
						<div id="popupArea" type="PurchaseFailed" >
							<div class="btn_popupclose"><a href="#">'.$lang['close'].'</a></div>
							<div class="popupText_box line2">
								<div class="popupTitle">'.$lang['error'].'!</div>
								<div class="popupText"></div>
							</div>
							<div class="btn_popup">
								<div class="buttonBox">
									<a href="#" class="btn_popupclose_B">'.$lang['close'].'</a></a>
								</div>
							</div>
						</div>
					</div>
					<div id="notice" style="display:none">   
						<div class="listBox_btm">
							<div class="innerBox"  style="padding-left:10px">
								'.$lang['notice'].'
							</div>
						</div>
					</div>
				</body>
				</html>';
	echo $content;
?>