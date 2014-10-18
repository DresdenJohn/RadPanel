<?php
	require_once( 'include/config.php' );

	$content = '';
	$_GET['itemname'] = '';
	$category = $shop_cat[substr( $_GET['categoryid'], 8 )]; //empty( $_GET['itemname'] ) ? $shop_cat[substr( $_GET['categoryid'], 8 )] : $lang['search'].' : '.$_GET['itemname'];
	$genderselect = !empty( $_GET['gender'] ) && is_numeric( $_GET['gender'] ) && $_GET['gender'] != 3  ? ' AND [dwGender] = '.$_GET['gender'] : '';
	$page = !empty( $_GET['currpage'] ) && is_numeric( $_GET['currpage'] ) && $_GET['currpage'] > 0 && $_GET['currpage'] < $_GET['currpage'] * 6 ? $_GET['currpage'] : 1;
	$page_max = $page * 6;
	$page_limit = $page_max - 6;
	
	$SQL = new MSSQL( true, true );
	//if( !empty( $_GET['itemname'] ) )
	//{
	//	$Query = "SELECT * FROM [HOMEPAGE_DBF].[dbo].[DONATE_SHOP_TBL] WHERE [dwSale] = 1";
	//	$result = $SQL->Exec( $Query ) ? $SQL->m_fetch() : false;
	//}
	//else
	//{
		$Query = "SELECT * FROM [HOMEPAGE_DBF].[dbo].[DONATE_SHOP_TBL] WHERE [dwCategory] = %s AND [dwSale] = 1".$genderselect;
		$result = $SQL->Exec( $Query, substr( $_GET['categoryid'], 8 ) ) ? $SQL->m_fetch() : false;
	//}
	
	$content .= '<div class="listBox_btm">
				<div class="innerBox">
					<h2>'.$category.'</h2>
					<ul>';
						for( $i = $page_limit; $i < $page_max; $i++ )
						{
							$class = '';
							if( $i == $page_limit )
								$class = ' class="first"';
							else if( $i % 2 != 0 )
								$class = ' class="right"';
							$SQL = new MSSQL( true );
							$Query = "SELECT [Index], [szName], [szIcon] FROM [ITEM_DBF].[dbo].[ITEM_TBL] WHERE [Index] = %s";
							$Item = $SQL->Exec( $Query, $result[$i]['dwItemId'] ) ? $SQL->fetch[0] : false;
							if( !$Item  )
								break;
							//if( !empty( $_GET['itemname'] ) )
							//{
							//	if( strpos( strtolower($Item['szName']), strtolower($_GET['itemname']) ) === false )
							//		break;
							//}
							if( strlen( $Item['szName'] ) > 30 )
								$Item['szName'] = substr( $Item['szName'], 0, 30 ).'...';
							$content .= '<li'.$class.'>
											<dt>'.$Item['szName'].'</dt>
											<dd>
												<div style="width: 58px; height: 48px; display:table-cell; vertical-align: middle; text-align: center;" id="imgiteminfo">
													<a href="#'.$result[$i]['SEQ'].'">
														<img src="images/items/'.substr( $Item['szIcon'], 0, -4 ).'.png" />
													</a>
												</div>
											</dd>
											<dd class="itemInfo">
												<span>'.$result[$i]['dwCost'].' '.$cash_name.'<br>Anzahl: '.$result[$i]['dwAmount'].'</span>
											</dd>
											<dl></dl>
											<div class="btnArea">
												<dd><a href="#'.$result[$i]['SEQ'].'" class="btn_gift">'.$lang['gift'].'</a></dd>
												<dd><a href="#'.$result[$i]['SEQ'].'" class="btn_buy">'.$lang['buy'].'</a></dd>
											</div>
										</li>';
						}
		$content .= '</ul>
					<div id="itemlistpasing" class="pasing">';
					$i = ( ceil( $_GET['currpage'] / 5 ) * 5 + 1 );
					$max = ceil( sizeof( $result ) / 6 );
					//if( $max > 1 )
					{
						if( $i - 5 > 5 )
							$content .= '<a class="prev" href="javascript:DisplayItemListAsync( \''.($i - 6).'\', \''.$_GET['categoryid'].'\', \''.$_GET['gender'].'\', \''.$_GET['itemname'].'\');"><span>'.$lang['prev'].'</span></a>';
						for( $j = $i - 5; $j < $i; $j++ )
						{
							if( $j > $max )
								break;
							if( $j == $_GET['currpage'] )
								$content .= '<strong>'.$j.'</strong>';
							else
								$content .= '<a href="javascript:DisplayItemListAsync( \''.$j.'\', \''.$_GET['categoryid'].'\', \''.$_GET['gender'].'\', \''.$_GET['itemname'].'\');">'.$j.'</a>';
						}
						if( $j < $max )
							$content .= '<a class="next" href="javascript:DisplayItemListAsync( \''.$j.'\', \''.$_GET['categoryid'].'\', \''.$_GET['gender'].'\', \''.$_GET['itemname'].'\');"><span>'.$lang['next'].'</span></a>';
					}
		$content .= '</div>
				</div>
			</div>';
		echo $content;
?>