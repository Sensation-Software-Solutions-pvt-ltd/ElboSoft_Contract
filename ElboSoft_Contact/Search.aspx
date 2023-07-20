<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ElboSoft_Contact.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        $(document).ready(() => {
            let template = ` <div class="form-group">
                    <label for="firstName" class="col-sm-5 control-label">&nbsp;</label>
                    <div class="col-sm-7 plus-control">
                            <select class="form-control" name="ContractTypeID" id="ContractTypeID">
                           </select> 
                             <span class="glyphicon pls-icon glyphicon-minus remove-block" ></span>                      
                    </div>


                </div>`;

            $("#add").on("click", () => {
                $("#items").append(template);
            })
            $("body").on("click", ".remove-block", (e) => {
                $(e.target).parent("div").parent('.form-group').remove();
            })
        });

    </script>
    <div class="container ">
        <main class="body_content">
            <div class="row">
                <div class="col-md-12 pageheader">
                    <h1>Search Form</h1>
                </div>
            </div>
            <div class="widget">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="email">Request Type</label>
                           <asp:DropDownList ID="RequestType" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="email">Request Number</label>
                            <asp:TextBox ID="RequestNumber" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="email">Customer</label>
                            <asp:DropDownList ID="Customer" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>


                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="email">Request Date</label>
                            <asp:TextBox runat="server" TextMode="Date" ID="RequestDate" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label class="checkbox-inline">
                            <asp:CheckBox ID="ContractCreated" runat="server" />
                        </label>
                    </div>
                </div>

            </div>



            <div class="tablebloxk">
                <div class="row">
                    <div class="col-md-12">

                            <asp:GridView ID="SearchGrid" runat="server" AutoGenerateColumns="false" OnRowDataBound="SearchGrid_RowDataBound" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Request Number">
                                    <ItemTemplate>
                                        <asp:TextBox ID="RequestNumber" runat="server" class="form-control" Text='<%# Bind("RequestNumber") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Customer" runat="server" class="form-control" Text='<%# Bind("Customer") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purpose">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Purpose" runat="server" class="form-control" Text='<%# Bind("Purpose") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Request Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="RequestDate" runat="server" class="form-control" Text='<%# Bind("RequestDate") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contract Created">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ContractCreated" runat="server" class="form-control" Text='<%# Bind("ContractCreated") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                   
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                      
                    </div>
                </div>


                <div class="action-bar">
                    <button class="btn btn-primary">Save</button>

                </div>





            </div>




        </main>

        <hr>
       
    </div>
</asp:Content>
