<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contract.aspx.cs" Inherits="ElboSoft_Contact.Contact1" %>

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

            // $("#add").on("click", ()=>{
            //     $("#items").append(template);
            // })
            // $("body").on("click", ".remove-block", (e)=>{
            //     $(e.target).parent("div").parent('.form-group').remove();
            // })

            $("#ad-ico").on("click", () => {
                $("#edited-form").show();
                $('#non-edited-form').hide();
            })



            $("#csadded").on("click", () => {
                $("#non-edited-form").show();
                $('#edited-form').hide();
            })
        });

    </script>
    <div class="container ">
        <main class="body_content">
            <div class="row">
                <div class="col-md-12 pageheader">
                    <h1>Contract Form</h1>
                </div>
            </div>
            <div class="widget">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="firstName" class="col-sm-5 control-label">Contract Type </label>
                                <div class="col-sm-7 plus-control" id="non-edited-form">
                                    <asp:DropDownList ID="ContractTypeID" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <span id="ad-ico" class=" pls-icon glyphicon glyphicon glyphicon-plus"></span>
                                </div>
                                <div class="col-sm-7 plus-control " id="edited-form" style="display: none;">
                                <asp:TextBox ID="Contractnumber" runat="server" CssClass="form-control"></asp:TextBox>
                                <span id="csadded" class=" pls-icon checkblock">&#x2714;</span>
                            </div>
                            </div>
                            
                            <div class="form-group">
                                <label for="firstName" class="col-sm-5 control-label">Contract Number</label>

                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" ID="Requestnumber" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="email" class="col-sm-5 control-label">Customer</label>
                                <div class="col-sm-7">
                                    <asp:DropDownList ID="CustomerID" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="email" class="col-sm-5 control-label">PaymentType</label>
                                <div class="col-sm-7">
                                    <asp:DropDownList ID="PaymentTypeID" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>


                            <div class="form-group">
                                <label for="password" class="col-sm-5 control-label">
                                    Advance Amount

                                </label>
                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" ID="AdvanceAmount" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="password" class="col-sm-5 control-label">
                                    No of Installments

                                </label>
                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" TextMode="Number" ID="InstallmentNo" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="password" class="col-sm-5 control-label">
                                    Total Amount Nedded

                                </label>
                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" ID="AmountNeeded" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="password" class="col-sm-5 control-label">
                                    Bank guarantee amount

                                </label>
                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" TextMode="Number" ID="BankGuaranteeAmount" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="CompartmentID" class="col-sm-5 control-label">
                                    Purpose
                                </label>
                                <div class="col-sm-7">
                                   <asp:DropDownList ID="PurposeList" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="CompartmentID" class="col-sm-5 control-label">
                                    Request Date
                                </label>
                                <div class="col-sm-7">
                                    <asp:TextBox TextMode="Date" runat="server"  ID="RequestDate" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!-- /form -->

                    </div>

                    <div class="col-md-5 ">

                        <div class="form-horizontal">
                            <div class="form-group checkbox-control">
                                <label for="IDCopyPresented" class="col-sm-6 control-label">
                                    ID Copy

                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:CheckBox runat="server" ID="IDCopy" />
                                </div>
                            </div>

                            <div class="form-group checkbox-control">
                                <label for="Bankac" class="col-sm-6 control-label">
                                    ID Bank account

                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:CheckBox runat="server" ID="BankAccountId" />
                                </div>
                            </div>
                            <div class="form-group checkbox-control">
                                <label for="PensionPresented" class="col-sm-6 control-label">
                                    Pension check
                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:CheckBox runat="server" ID="PensionCheck" />
                                </div>
                            </div>

                            <div class="form-group checkbox-control">
                                <label for="PensionPresented" class="col-sm-6 control-label">
                                    Central register copy

                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:CheckBox runat="server" ID="RegisterCopy" />
                                </div>
                            </div>

                            <div class="form-group checkbox-control">
                                <label for="PensionPresented" class="col-sm-6 control-label">
                                    Power of attorny

                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:CheckBox runat="server" ID="PowerAttorney" />
                                </div>
                            </div>
                            <div class="form-group checkbox-control">
                                <label for="PensionPresented" class="col-sm-6 control-label">
                                    Affidavit
                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:CheckBox runat="server" ID="Affiadavit" />
                                </div>
                            </div>
                            <div class="form-group checkbox-control">
                                <label for="PensionPresented" class="col-sm-6 control-label">
                                    Confirmation

                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:CheckBox runat="server" ID="Confirmation" />
                                </div>
                            </div>

                            <div class="form-group checkbox-control">
                                <label for="PensionPresented" class="col-sm-6 control-label">
                                    DRD Form

                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:CheckBox runat="server" ID="DRDForm" />
                                </div>
                            </div>

                            <div class="form-group checkbox-control">
                                <label for="PensionPresented" class="col-sm-6 control-label">
                                    Declaration of receipt

                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:CheckBox runat="server" ID="DeclarationReceipt" />
                                </div>
                            </div>

                            <div class="form-group checkbox-control">
                                <label for="PensionPresented" class="col-sm-6 control-label">
                                    Agreement
                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:CheckBox runat="server" ID="Agreement" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tablebloxk">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="ContractGrid" runat="server" AutoGenerateColumns="false" OnRowDataBound="ContractGrid_RowDataBound" ShowHeaderWhenEmpty="true" CssClass="table table-bordered">
                            <Columns>
                                <asp:TemplateField HeaderText="Regional Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ReginalCenter" runat="server" class="form-control"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Management Unit">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ManagementUnit" runat="server" class="form-control"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Compartment">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Compartment" TextMode="Number" runat="server" class="form-control" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SubCompartment">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SubcompartmentID" TextMode="Number" runat="server" class="form-control" Text='<%# Bind("SubcompartmentID") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="Month" runat="server" class="form-control"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edinecna mera">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="Edinecnamera" runat="server" class="form-control"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vid sortiment">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="Vidsortiment" runat="server" class="form-control"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Qty" runat="server" TextMode="Number" class="form-control" Text='<%# Bind("Qty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Price" TextMode="Number" runat="server" class="form-control" Text='<%# Bind("Price") %>'></asp:TextBox>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" CssClass="btn btn-btn-default"/>
                    </div>
                </div>

                <div class="action-bar" runat="server" id="adddiv">
                    <asp:Button ID="Savebutton" runat="server" OnClick="Savebutton_Click" CssClass="btn btn-primary" Text="Save" />
                </div>
                <div class="action-bar" runat="server" id="updatediv" visible="false">
                    <asp:Button ID="updateContract" runat="server" OnClick="updateContract_Click" CssClass="btn btn-primary" Text="Update" />
                </div>
            </div>
        </main>
        <hr>
    </div>
</asp:Content>
