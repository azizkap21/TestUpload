<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="TestAsyncWeb.Products.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <button type="button" class="btn btn-primary" id="btnGetProducts" >Get Products</button>

    <div class="container">

        <table class="table table-stripe">

            <thead>
                <tr>
                    <th>
                        Product Name
                    </th>
                    <th>
                        Brand
                    </th>
                    <th>
                        Category
                    </th>
                    <th>
                        Price
                    </th>
                </tr>
            </thead>
            <tbody>

            </tbody>

        </table>

    </div>

    <pre id="products">

    </pre>

    <div class="container">
        <button id="btnGetTime" class="btn btn-default">
            Get Time
        </button>

    <label id="lblTime" class="control-label">Current Time</label>

    </div>


</asp:Content>
