<?php
	require_once( 'include/config.php' );
	require_once( 'include/class/class.dpsrvr.php' );
	
	$return = 0;
	$SQL = new MSSQL( true );
	$Query = "SELECT [cash] FROM [ACCOUNT_DBF].[dbo].[ACCOUNT_TBL] WHERE [account] = %s";
	$cash = $SQL->Exec( $Query, $_SESSION['user_id'] ) ? $SQL->fetch[0]['cash'] : false;
	if( $cash )
	{
		$SQL = new MSSQL( true );
		$Query = "SELECT * FROM [HOMEPAGE_DBF].[dbo].[DONATE_SHOP_TBL] WHERE [SEQ] = %s AND [dwSale] = 1";
		$result = $SQL->Exec( $Query, $_GET['itemid'] ) ? $SQL->fetch[0] : false;
		if( $result && $result['dwCost'] <= $cash )
		{
			$SQL = new MSSQL( true );
			$Query = "SELECT [Index], [szName], [szIcon], [szComment] FROM [ITEM_DBF].[dbo].[ITEM_TBL] WHERE [Index] = %s";
			$Item = $SQL->Exec( $Query, $result['dwItemId'] ) ? $SQL->fetch[0] : false;
			if( $Item )
			{
				if( $_GET['presentflag'] == 1 )
				{
					$SQL = new MSSQL( true );
					$Query = "SELECT [m_idPlayer], [MultiServer] FROM [CHARACTER_01_DBF].[dbo].[CHARACTER_TBL] WHERE [account] = %s AND [m_idPlayer] = %s AND [isblock] = 'F'";
					$Character = $SQL->Exec( $Query, $_SESSION['user_id'], sprintf( "%07d", $_SESSION['m_idPlayer'] ) ) ? $SQL->fetch[0] : false;
					if( $Character )
					{
						if( $Character['MultiServer'] == 0 )
						{
							$SQL = new MSSQL();
							$Query = "USE [CHARACTER_01_DBF] EXEC [dbo].[uspProvideItemToCharacter] @pPlayerID = %s, @pserverindex = '01', @pItemIndex = %s, @pItemCnt = %s";
							$SQL->Exec( $Query, $Character['m_idPlayer'], $result['dwItemId'], $result['dwAmount'] );
							$SQL = new MSSQL();
							$Query = "UPDATE [ACCOUNT_DBF].[dbo].[ACCOUNT_TBL] SET [cash] = ( [cash] - %s ) WHERE [account] = %s";
							$SQL->Exec( $Query, $result['dwCost'], $_SESSION['user_id'] );
						}
						else
						{
							if( CDPSrvr::CreateItem( $Character['m_idPlayer'], $result['dwItemId'], $result['dwAmount'] ) )
							{
								$SQL = new MSSQL();
								$Query = "UPDATE [ACCOUNT_DBF].[dbo].[ACCOUNT_TBL] SET [cash] = ( [cash] - %s ) WHERE [account] = %s";
								$SQL->Exec( $Query, $result['dwCost'], $_SESSION['user_id'] );
							}
						}
						$SQL = new MSSQL();
						$Query = "UPDATE [HOMEPAGE_DBF].[dbo].[DONATE_SHOP_TBL] SET [dwPurchase] = ( [dwPurchase] + 1 ) WHERE [SEQ] = %s";
						$SQL->Exec( $Query, $result['SEQ'] );
						$SQL = new MSSQL();
						$Query = "INSERT INTO [HOMEPAGE_DBF].[dbo].[DONATE_SHOP_LOG_TBL] ( [m_idPlayerFrom], [m_idPlayerTo], [dwGift], [dwItemId], [dwCost], [dwAmount], [DateTime] ) VALUES ( %s, %s, 0, %s, %s, %s, GETDATE() )";
						$SQL->Exec( $Query, $Character['m_idPlayer'], '0000000', $result['dwItemId'], $result['dwCost'], $result['dwAmount'] );
						$return = 1;
					}
				}
				else if( $_GET['presentflag'] == 2 )
				{
					$SQL = new MSSQL( true );
					$Query = "SELECT [account], [m_idPlayer], [MultiServer] FROM [CHARACTER_01_DBF].[dbo].[CHARACTER_TBL] WHERE [m_idPlayer] = %s AND [isblock] = 'F'";
					$Character = $SQL->Exec( $Query, sprintf( "%07d", $_GET['f_character'] ) ) ? $SQL->fetch[0] : false;
					if( $Character )
					{
						if( $Character['MultiServer'] == 0 )
						{
							$SQL = new MSSQL();
							$Query = "USE [CHARACTER_01_DBF] EXEC [dbo].[uspProvideItemToCharacter] @pPlayerID = %s, @pserverindex = '01', @pItemIndex = %s, @pItemCnt = %s";
							$SQL->Exec( $Query, $Character['m_idPlayer'], $result['dwItemId'], $result['dwAmount'] );
							$SQL = new MSSQL();
							$Query = "UPDATE [ACCOUNT_DBF].[dbo].[ACCOUNT_TBL] SET [cash] = ( [cash] - %s ) WHERE [account] = %s";
							$SQL->Exec( $Query, $result['dwCost'], $_SESSION['user_id'] );
						}
						else
						{
							if( CDPSrvr::CreateItem( $Character['m_idPlayer'], $result['dwItemId'], $result['dwAmount'] ) )
							{
								$SQL = new MSSQL();
								$Query = "UPDATE [ACCOUNT_DBF].[dbo].[ACCOUNT_TBL] SET [cash] = ( [cash] - %s ) WHERE [account] = %s";
								$SQL->Exec( $Query, $result['dwCost'], $_SESSION['user_id'] );
							}
						}
						$SQL = new MSSQL();
						$Query = "UPDATE [HOMEPAGE_DBF].[dbo].[DONATE_SHOP_TBL] SET [dwPurchase] = ( [dwPurchase] + 1 ) WHERE [SEQ] = %s";
						$SQL->Exec( $Query, $result['SEQ'] );
						$SQL = new MSSQL();
						$Query = "INSERT INTO [HOMEPAGE_DBF].[dbo].[DONATE_SHOP_LOG_TBL] ( [m_idPlayerFrom], [m_idPlayerTo], [dwGift], [dwItemId], [dwCost], [dwAmount], [DateTime] ) VALUES ( %s, %s, 1, %s, %s, %s, GETDATE() )";
						$SQL->Exec( $Query, sprintf( "%07d", $_SESSION['m_idPlayer'] ), $Character['m_idPlayer'], $result['dwItemId'], $result['dwCost'], $result['dwAmount'] );
						$return = 1;
					}
				}
			}
		}
	}
	echo $return;
?>