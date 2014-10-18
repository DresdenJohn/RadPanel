<?php
	if( isset( $_POST['user_id'] ) )
		session_unset();
	$_SESSION['user_id'] = isset( $_SESSION['user_id'] ) ? $_SESSION['user_id'] : $_POST['user_id'];
	$_SESSION['m_idPlayer'] = isset( $_SESSION['m_idPlayer'] ) ? $_SESSION['m_idPlayer'] : $_POST['m_idPlayer'];
	$_SESSION['server_index'] = isset( $_SESSION['server_index'] ) ? $_SESSION['server_index'] : $_POST['server_index'];
	$_SESSION['check'] = isset( $_SESSION['check'] ) ? $_SESSION['check'] : $_POST['check'];
	$_SESSION['md5'] = isset( $_SESSION['md5'] ) ? $_SESSION['md5'] : $_POST['md5'];
	
	if( !empty( $_SESSION['user_id'] ) )
	{
		$SQL = new MSSQL( true );
		$Query = "SELECT * FROM [ACCOUNT_DBF].[dbo].[ACCOUNT_TBL] WHERE [account] = %s";
		$account = $SQL->Exec( $Query, $_SESSION['user_id'] ) ? $SQL->fetch[0] : false;
		if( $account )
		{
			if( !is_numeric( $_SESSION['m_idPlayer'] ) )
			{
				$SQL = new MSSQL( true );
				$Query = "SELECT [m_idPlayer] FROM [CHARACTER_01_DBF].[dbo].[CHARACTER_TBL] WHERE [account] = %s AND [isblock] = 'F'";
				$_SESSION['m_idPlayer'] = $SQL->Exec( $Query, $_SESSION['user_id'] ) ? (int)$SQL->fetch[0]['m_idPlayer'] : false;
			}
			if( md5( $_SESSION['user_id'].$_SESSION['m_idPlayer'].$_SESSION['server_index'].$hash_shop ) == $_SESSION['md5'] )
			{
				$SQL = new MSSQL( true );
				$Query = "SELECT * FROM [CHARACTER_01_DBF].[dbo].[CHARACTER_TBL] WHERE [account] = %s AND [m_idPlayer] = %s AND [isblock] = 'F'";
				$character = $SQL->Exec( $Query, $_SESSION['user_id'], sprintf( "%07d", $_SESSION['m_idPlayer'] ) ) ? $SQL->fetch[0] : false;
				if( $character )
				{
					$_SESSION['user_id'] = $_SESSION['user_id'];
					$_SESSION['m_idPlayer'] = $_SESSION['m_idPlayer'];
					$_SESSION['server_index'] = $_SESSION['server_index'];
					$_SESSION['check'] = $_SESSION['check'];
					$_SESSION['md5'] = md5( $_SESSION['user_id'].$_SESSION['m_idPlayer'].$_SESSION['server_index'].$hash_shop );
				}
				else
				{
					die( $lang['error_1'] );
				}
			}
			else
			{
				die( $lang['error_2'] );
			}
		}
		else
		{
			die( $lang['error_3'] );
		}
	}
	else
	{
		die( $lang['error_4'] );
	}
?>