<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ElboSoft_Contact.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function deleterecord(requestNo) {
            if (confirm("Are you sure you want to delete this Request Number")) {
                 document.getElementById("<%=hiddenrquestno.ClientID %>").value=requestNo;
                document.getElementById("<%=deleterecord.ClientID %>").click();
                
            };
        }
    </script>
    <div class="container ">
        <main class="body_content">
            <div class="row">
                <div class="col-md-12 pageheader">
                    <h1>Search Form</h1>
                </div>
            </div>
            <asp:HiddenField ID="hiddenrquestno" runat="server" />
            <div class="widget">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Request Type</label>
                           <asp:DropDownList ID="RequestType" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Request Number</label>
                            <asp:TextBox ID="RequestNumber" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Customer</label>
                            <asp:DropDownList ID="Customer" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>


                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Request Date</label>
                            <asp:TextBox runat="server" TextMode="Date" ID="RequestDate" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label class="checkbox-inline">
                            <asp:CheckBox ID="ContractCreated" runat="server" /> Contact Created
                        </label>
                    </div>
                    <div>
                        <asp:button CssClass="btn btn-primary" Text="Search" runat="server" ID="btnSearch" OnClick="btnSearch_Click"></asp:button>
                    </div>
                </div>

            </div>



            <div class="tablebloxk">
                <div class="row">
                    <div class="col-md-12">

                            <asp:GridView ID="SearchGrid" runat="server" AutoGenerateColumns="false" OnRowDataBound="SearchGrid_RowDataBound" ShowHeaderWhenEmpty="true" CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField HeaderText="Request Number" DataField="RequestNumber" />
                                <asp:BoundField HeaderText="Customer" DataField="CustomerID" />
                                <asp:BoundField HeaderText="Purpose" DataField="PurposeID" />
                                <asp:BoundField HeaderText="Request Date" DataField="RequestDate" />
                                <asp:BoundField HeaderText="" DataField="RequestHeaderID" Visible="false" />
                                <%--<asp:BoundField HeaderText="Contract Created" DataField="IsCreatedContract" />--%>

                             <%--   <asp:TemplateField HeaderText="Request Number">

                                    <ItemTemplate>
                                        <asp:TextBox ID="RequestNumber" runat="server" ReadOnly="true" class="form-control" Text='<%# Bind("RequestHeaderID") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Customer" runat="server" ReadOnly="true" class="form-control" Text='<%# Bind("CustomerID") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purpose">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Purpose" runat="server" ReadOnly="true" class="form-control" Text='<%# Bind("PurposeID") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Request Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="RequestDate" runat="server" ReadOnly="true" class="form-control" Text='<%# Bind("RequestDate") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Contract Created">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" CssClass='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"IsCreatedContract"))==true?"fa fa-check green":"" %>' ID="contract" ></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                   <ItemTemplate>
                                       <asp:HyperLink NavigateUrl='<%# String.Format("{0}={1}",Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCreatedContract"))==true?"Contract.aspx?ContractId":"Request.aspx?RequestId",Convert.ToInt32(DataBinder.Eval(Container.DataItem, "RequestHeaderID"))==0?DataBinder.Eval(Container.DataItem,"ContractHeaderId")+"c":DataBinder.Eval(Container.DataItem,"RequestHeaderId")) %>'   runat="server" ID="RequestEdit" CssClass="blue fa fa-edit" ></asp:HyperLink>
                                       <%-- <span style="color:indianred"><i class="fa fa-trash" onclick="deleterecord('<%# DataBinder.Eval(Container.DataItem, "RequestHeaderID") %>'"></i></span>--%>
                                       <a href="#" onclick="deleterecord('<%# DataBinder.Eval(Container.DataItem, "RequestHeaderID") %>')" style="color:indianred" class='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"IsCreatedContract"))==false?"fa fa-trash":"" %>'></a>
                                   </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                      
                    </div>
                </div>

                <asp:Button ID="deleterecord" runat="server" OnClick="deleterecord_Click" style="display:none"/>
               
            </div>
        </main>

        <hr>
       
    </div>
</asp:Content>
