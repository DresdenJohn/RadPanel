<?php
	require_once( 'include/config.php' );
	
	if( empty( $_GET['itemid'] ) )
	{
		$SQL = new MSSQL( true );
		$Query = "SELECT TOP 1 * FROM [HOMEPAGE_DBF].[dbo].[DONATE_SHOP_TBL] WHERE [dwSale] = 1 ORDER BY [dwPurchase] DESC";
		$_GET['itemid'] = $SQL->Exec( $Query, $_GET['itemid'] ) ? $SQL->fetch[0]['SEQ'] : false;
	}
	$content = '';
	$SQL = new MSSQL( true );
	$Query = "SELECT * FROM [HOMEPAGE_DBF].[dbo].[DONATE_SHOP_TBL] WHERE [SEQ] = %s AND [dwSale] = 1";
	$result = $SQL->Exec( $Query, $_GET['itemid'] ) ? $SQL->fetch[0] : false;
	$SQL = new MSSQL( true );
	$Query = "SELECT [Index], [szName], [szIcon], [szComment] FROM [ITEM_DBF].[dbo].[ITEM_TBL] WHERE [Index] = %s";
	$Item = $SQL->Exec( $Query, $result['dwItemId'] ) ? $SQL->fetch[0] : false;
	
	$content .= '<div class="infoBox_btm">
				<div class="innerBox">
					<h2>'.$lang['details'].'</h2>
					<div class="itemDetail">
						<ul>
							<li class="first">
								<dt>'.$Item['szName'].'</dt>
								<br />
								<dd>
									<div style="width: 58px; height: 48px; display:table-cell; vertical-align: middle; text-align: center;">
										<img src="images/items/'.substr( $Item['szIcon'], 0, -4 ).'.png" />
									</div>
								</dd>
								<dd class="itemInfo">
									<span>'.$result['dwCost'].' '.$cash_name.'<br>Anzahl: '.$result['dwAmount'].'</span>
								</dd>
								<dl></dl>
							</li>
							<li>
								<div class="detailText">
									<div class="scrollBox">
										'.$Item['szComment'].'
									</div>
								</div>
								<dl></dl>
							</li>
							<li>
								<div class="priceArea">';
									$SQL = new MSSQL( true );
									$Query = "SELECT [cash] FROM [ACCOUNT_DBF].[dbo].[ACCOUNT_TBL] WHERE [account] = %s";
									$cash = $SQL->Exec( $Query, $_SESSION['user_id'] ) ? $SQL->fetch[0]['cash'] : 0;
									if( $_GET['sendgiftflag'] == 1 )
									{
										$content .= '<ul>
														<li>'.$lang['price'].':<span>'.$result['dwCost'].' '.$cash_short.'</span></li>
														<li>'.$lang['remaining'].':<span>'.$cash.' '.$cash_short.'</span></li>
													</ul>
													<div class="btnArea">
														<a href="#'.$result['SEQ'].'" class="btn_detailBuy">'.$lang['buy'].'</a>
													</div>';
									}
									else if( $_GET['sendgiftflag'] == 2 )
									{
										$SQL = new MSSQL( true, true );
										$Query = "SELECT a.[idFriend], b.[m_szName] FROM [CHARACTER_01_DBF].[dbo].[tblMessenger] a INNER JOIN [CHARACTER_01_DBF].[dbo].[CHARACTER_TBL] b ON ( a.[idFriend] = b.[m_idPlayer] ) WHERE a.[idPlayer] = %s AND a.[chUse] = 'T' AND b.[isblock] = 'F' ORDER BY b.[m_szName]";
										$Friend = $SQL->Exec( $Query, sprintf( "%07d", $_SESSION['m_idPlayer'] ) ) ? $SQL->m_fetch() : false;
										if( !$Friend )
										{
											$content .= '<div id="friendselect" class="selectFriend" style="width:167px">
															<span class="selectForm"> 
																<span class="selected" style="width:134px">'.$lang['nofriends'].'</span>
															</span>
															<span class="selectMenu"></span>
														</div>
														<ul>
															<li>'.$lang['price'].':<span>'.$result['dwCost'].' '.$cash_short.'</span></li>
															<li>'.$lang['remaining'].':<span>'.$cash.' '.$cash_short.'</span></li>
														</ul>
														<div class="btnArea" id="giftbtnArea">
															<a href="#'.$result['SEQ'].'" class="btn_detailgift">'.$lang['gift'].'</a>
														</div>';

										}
										else
										{
											$content .= '<div id="friendselect" class="selectFriend" style="width:167px">
															<span class="selectForm"> 
																<span class="selected" style="width:134px">'.$lang['selfriend'].'</span>
																<span class="list" style="display:none; min-height:80px; max-height:80px; padding-right:6px; overflow-y:scroll;">';
																	for( $i = 0; $i < sizeof( $Friend ); $i++ )
																	{
																		$content .= '<a href="#'.$Friend[$i]['idFriend'].'">'.$Friend[$i]['m_szName'].'</a>';
																	}
													$content .= '</span> 
															</span>
															<span class="selectMenu"></span>
														</div>
														<ul>
															<li>'.$lang['price'].':<span>'.$result['dwCost'].' '.$cash_short.'</span></li>
															<li>'.$lang['remaining'].':<span>'.$cash.' '.$cash_short.'</span></li>
														</ul>
														<div class="btnArea" id="giftbtnArea">
															<a href="#'.$result['SEQ'].'" class="btn_detailgift">'.$lang['gift'].'</a>
														</div>';
										}
									}
						$content .= '</div>
								<dl></dl>
							</li>
						</ul>
					</div>
				</div>
			</div>';
	echo $content;
?>