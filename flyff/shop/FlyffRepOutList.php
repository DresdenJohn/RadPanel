<?php
	require_once( 'include/config.php' );
	
	$content = '';
	$page = !empty( $_GET['currpage'] ) && is_numeric( $_GET['currpage'] ) && $_GET['currpage'] > 0 && $_GET['currpage'] < $_GET['currpage'] * 5 ? $_GET['currpage'] : 1;
	$page_max = $page * 5;
	$page_limit = $page_max - 5;
	$_GET['year'] = empty( $_GET['year'] ) ? sprintf( "%04d", date( 'Y' ) ) : sprintf( "%04d", $_GET['year'] );
	$_GET['month'] = empty( $_GET['month'] ) ? sprintf( "%02d", date( 'm' ) ) : sprintf( "%02d", $_GET['month'] );
	$_GET['day1'] = empty( $_GET['day1'] ) ? '01' : sprintf( "%02d", $_GET['day1'] );
	$_GET['day2'] = empty( $_GET['day2'] ) ? sprintf( "%02d", date( 'd' ) ) : sprintf( "%02d", $_GET['day2'] );
	$SQL = new MSSQL( true, true );
	$Query = "SELECT * FROM [HOMEPAGE_DBF].[dbo].[DONATE_SHOP_LOG_TBL] WHERE [m_idPlayerFrom] = %s AND YEAR( [DateTime] ) = '".$_GET['year']."' AND MONTH( [DateTime] ) = '".$_GET['month']."' AND DAY( [DateTime] ) >= '".$_GET['day1']."' AND DAY( [DateTime] ) <= '".$_GET['day2']."' ORDER BY [DateTime] DESC";
	$result = $SQL->Exec( $Query, $_SESSION['m_idPlayer'] ) ? $SQL->m_fetch() : false;
	$SQL = new MSSQL( true );
	$Query = "SELECT [m_szName] FROM [CHARACTER_01_DBF].[dbo].[CHARACTER_TBL] WHERE [m_idPlayer] = %s";
	$szNameFrom = $SQL->Exec( $Query, $_SESSION['m_idPlayer'] ) ? $SQL->fetch[0]['m_szName'] : false;
	
	$content .= '<div class="myListbox">
					<div class="myList">
						<div class="innerBox">
							<h2>'.$lang['pursum'].'</h2>
							<div class="searchDate">
								<ul>
									<li><span class="dateText">'.$lang['timespan'].'</span></li>
									<li><span class="dateText">'.$lang['y'].'</span></li>
									<li>
										<span class="dateText">
											<span class="selectForm"> 
												<span id="year" class="selected" style="width:33px">'.( empty( $_GET['year'] ) ? date( 'Y' ) : $_GET['year'] ).'</span>
												<span class="list" style="display:none;" >';
												for( $i = date( 'Y' ) - 2; $i <= date( 'Y' ); $i++ )
													$content .= '<a href="#'.sprintf( "%04d", $i ).'">'.sprintf( "%04d", $i ).'</a>';
									$content .= '</span>
											</span>
										</span>
									</li>
									<li><span class="dateText">'.$lang['m'].'</span></li>
									<li>
										<span class="dateText">
											<span class="selectForm"> 
												<span id="month" class="selected" style="width:23px">'.( empty( $_GET['month'] ) ? date( 'm' ) : $_GET['month'] ).'</span>
												<span class="list"  style="display:none;" >';
												for( $i = 1; $i <= 12; $i++ )
													$content .= '<a href="#'.sprintf( "%02d", $i ).'">'.sprintf( "%02d", $i ).'</a>';
									$content .= '</span>
											</span>
										</span>
									</li>
									<li><span class="dateText">'.$lang['d'].'</span></li>
									<li>
										<span class="dateText">
											<span class="selectForm"> 
												<span id="day1" class="selected" style="width:23px">01</span>
												<span class="list" style="display:none; min-height:240px; max-height:240px; padding-right:6px; overflow-y:scroll;">';
												for( $i = 1; $i <= 31; $i++ )
													$content .= '<a href="#'.sprintf( "%02d", $i ).'">'.sprintf( "%02d", $i ).'</a>';
									$content .= '</span>
											</span>
										</span>
									</li>
									<li><span class="dateText">~</span></li>
									<li>
										<span class="dateText">
											<span class="selectForm"> 
												<span id="day2" class="selected" style="width:23px">'.( empty( $_GET['day2'] ) ? date( 'd' ) : $_GET['day2'] ).'</span>
												<span class="list" style="display:none; min-height:240px; max-height:240px; padding-right:6px; overflow-y:scroll;">';
												for( $i = 1; $i < 31; $i++ )
													$content .= '<a href="#'.sprintf( "%02d", $i ).'">'.sprintf( "%02d", $i ).'</a>';
									$content .= '</span>
											</span>
										</span>
									</li>
									<li><a class="btn_search" href="javascript:showPopPurchaseHistory( \'1\' );">Suchen</a></li>
								</ul>
							</div>
							<div class="purchaseList">
								<table class="datatable">
									<thead>
										<tr>
											<th>'.$lang['item'].'</th>
											<th>'.$lang['used'].' '.$cash_name.'</th>
											<th>'.$lang['present'].'</th>
											<th>'.$lang['to'].'</th>
											<th>'.$lang['datetime'].'</th>
										</tr>
									</thead>
									<tbody>';
										for( $i = $page_limit; $i < $page_max; $i++ )
										{
											$SQL = new MSSQL( true );
											$Query = "SELECT [Index], [szName], [szIcon] FROM [ITEM_DBF].[dbo].[ITEM_TBL] WHERE [Index] = %s";
											$Item = $SQL->Exec( $Query, $result[$i]['dwItemId'] ) ? $SQL->fetch[0] : false;
											if( !$Item )
												break;
											$szGift = '-';
											$szNameTo = '';
											if( $result[$i]['dwGift'] == 1 )
											{
												$szGift = $lang['giveaway'];
												$SQL = new MSSQL( true );
												$Query = "SELECT [m_szName] FROM [CHARACTER_01_DBF].[dbo].[CHARACTER_TBL] WHERE [m_idPlayer] = %s";
												$szNameTo = $SQL->Exec( $Query, $result[$i]['m_idPlayerTo'] ) ? $SQL->fetch[0]['m_szName'] : '';
											}
											$content .= '<tr>
															<td>'.$Item['szName'].'</td>
															<td>'.$result[$i]['dwCost'].' '.$cash_name.'</td>
															<td>'.$szGift.'</td>
															<td>'.$szNameTo.'</td>
															<td>'.date( $lang['dateformat'], strtotime( $result[$i]['DateTime'] ) ).'</td>
														</tr>';
										}
						$content .= '</tbody>
								</table>
							</div>
						<div class="pasing">';
					$i = ( ceil( $_GET['currpage'] / 5 ) * 5 + 1 );
					$max = ceil( sizeof( $result ) / 5 );
					//if( $max > 1 )
					{
						if( $i - 5 > 5 )
							$content .= '<a class="prev" href="javascript:showPopPurchaseHistory( \''.($i - 6).'\' );"><span>Prev'.$lang['prev'].'</span></a>';
						for( $j = $i - 5; $j < $i; $j++ )
						{
							if( $j > $max )
								break;
							if( $j == $_GET['currpage'] )
								$content .= '<strong>'.$j.'</strong>';
							else
								$content .= '<a href="javascript:showPopPurchaseHistory( \''.$j.'\' );">'.$j.'</a>';
						}
						if( $j < $max )
							$content .= '<a class="next" href="javascript:showPopPurchaseHistory( \''.$j.'\' );"><span>'.$lang['next'].'</span></a>';
					}
		$content .= '</div>
						</div>
					</div>
				</div>';
	echo $content;
?>