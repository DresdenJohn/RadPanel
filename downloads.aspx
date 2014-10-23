<%@ Page Title="" Language="VB" MasterPageFile="~/WebMaster.master" AutoEventWireup="false" CodeFile="downloads.aspx.vb" Inherits="downloads" %>

<asp:Content ContentPlaceHolderID="title" runat="server" ID="content3">
    Downloads
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceholder" Runat="Server">
    
    <div class="box">
        <h2>Radical Flyff Downloads</h2>
        <hr />

        <style type="text/css">
            /* Styling Just for Downloads */

            .btn_download {
                margin: 20px auto;
                height: 100px;
                text-align: center;
                width: 300px;
                border: 2px solid #EEE;
                background: url('./Assets/downloadButton1.png') center center;
                color: black;
                box-shadow: 3px 3px 6px rgba(0,0,0,0.4) inset;
            }

            .btn_download2, .btn_download3 {
                margin: 20px auto;
                height: 150px;
                text-align: center;
                width: 200px;
                border: 2px solid #EEE;
                background: url('./Assets/downloadButton2.png') center center;
                color: black;
                box-shadow: 3px 3px 6px rgba(0,0,0,0.4) inset;
                display: inline-block;
            }
            .btn_download2 {
                margin-right: 25px;
            }

            .downloadLinks a {
                display: block;
                height: 30px;
                width: 200px;
                padding-top: 120px;
                font-size: 12px;
            }
            .downloadLinks2 a {
                display: block;
                height: 30px;
                width: 300px;
                padding-top: 41px;
                font-size: 12px;
            }
            .downloadButtons a:link, .downloadButtons a:active, .downloadButtons a:visited {
                color: black !important;
            }
            .downloadButtons a:hover {
                color: yellow !important;
            }
            #requirementsTable tr th[scope=col] {
                font-size: 15px;
                color: rgb(255, 192, 54);
                text-shadow: 3px 3px 3px rgba(0,0,0,0.7);
            }
            #requirementsTable tr th[scope=row] {
                font-size: 12px;
                color: #3194E7;
                text-shadow: 3px 3px 3px rgba(0,0,0,0.7);
                text-align:right;
                padding-right: 10px;
            }
            #requirementsTable tr td {
                font-size: 10px;
                color: rgba(255,255,255,0.5);
                text-shadow: 2px 2px 7px rgba(0,0,0,.8);
            }


        </style>

        <div class="section_client_dnBox">
            
            <h4 class="headingType08" style="text-align:center">System Requirements</h4>
            <hr />

			<div class="requirements">
				<table id="requirementsTable">
					<thead>
						<tr>
							<th scope="col" class="first" style="width: 150px;"></th>
							<th scope="col" style="width:200px;">Minimum</th>
							<th scope="col">Suggested</th>
						</tr>
					</thead>
					<tbody>
						<tr>
							<th scope="row">CPU</th>
							<td>Pentium 4 2.0 GHz</td>
							<td>Pentium 4 3.0 GHz</td>
						</tr>
						<tr>
							<th scope="row">RAM</th>
							<td>1GB</td>
							<td>2GB</td>
						</tr>
						<tr>
							<th scope="row">Video Card</th>
							<td>ATI Radeon 9500 / nVidia 5200 - 128mb memory</td>
							<td>Geforce 6600 GT / 256 mb video memory or better</td>
						</tr>
						<tr>
							<th scope="row">Operating System</th>
							<td>Windows XP, Vista, 7, 8, 8.1, 10 (Tech Preview)</td>
							<td>Windows XP, Vista, 7, 8, 8.1, 10 (Tech Preview)</td>
						</tr>
						<tr>
							<th scope="row">Hard Drive</th>
							<td>3GB</td>
							<td>4GB</td>
						</tr>
						<tr>
							<th scope="row">DirectX</th>
							<td>DirectX 9.0</td>
							<td>DirectX 9.0</td>
						</tr>
					</tbody>
				</table>
			</div>

            <hr />
			<p style="text-align:center">Download the <strong>Radical Flyff Client</strong> below to start playing.</p>
			<div class="btn_download downloadLinks2 downloadButtons" style="background: url('./Assets/downloadButton5.png') center center;">
				<a href="#NotYetAmigo" target="_blank"><span style="padding-left: 90px;">Download From Mega</span></a>
			</div>
            <div class="btn_download downloadLinks2 downloadButtons" style="background: url('./Assets/downloadButton6.png') center center;">
				<a href="#Nope.avi" target="_blank"><span style="padding-left: 90px;">Download From SpeedyShare</span></a>
			</div>
			
		</div>

        <asp:Literal ID="downloadLiteral" runat="server" Visible="false"></asp:Literal>

        
    </div>

</asp:Content>

