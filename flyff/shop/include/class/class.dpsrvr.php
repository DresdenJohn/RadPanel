<?php
	/****** Class: CDPSrvr	Script Date: 04/01/2014 ******/
	class CDPSrvr
	{
		// API
		private function SendAPICmd( $ServerIndex, $dwPlayerId, $dwTargetId, $dwParam1, $dwParam2, $dwParam3 )
		{
			$socket = socket_create( AF_INET, SOCK_STREAM, SOL_TCP );
			$packet = pack( "VVVVVVVV", $ServerIndex, $dwPlayerId, $dwTargetId, $dwParam1, $dwParam2, $dwParam3, BILLING_CHECK1, BILLING_CHECK2 );
			if( @socket_connect( $socket, BILLING_IP, BILLING_PORT ) )
			{
				socket_write( $socket, $packet, strlen( $packet ) );
				socket_close( $socket );
				return true;
			}
			return false;
		}
		// Create Item
		public static function CreateItem( $dwTargetId, $dwItemId, $dwItemCount )
		{
			if( !is_numeric( $dwItemId ) )
			{
				$SQL = new MSSQL( true );
				$Query = "USE [ITEM_DBF] SELECT * FROM [ITEM_TBL] WHERE [szName] = %s";
				$dwItemId = $SQL->Exec( $Query, $dwItemId ) ? $SQL->fetch[0]['dwID'] : false;
				if( !is_numeric( $dwItemId ) )
					return false;
			}
			return CDPSrvr::SendAPICmd( 01, (int)$dwTargetId, (int)$dwTargetId, $dwItemId, $dwItemCount, 0 );
		}
	}
?>