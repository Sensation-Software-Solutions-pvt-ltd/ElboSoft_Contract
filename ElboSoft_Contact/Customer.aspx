<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="ElboSoft_Contact.Customer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">

     $(document).ready(() => {
         let template = ` <div class="form-group">
                    <label class="col-sm-5 control-label">&nbsp;</label>
                    <div class="col-sm-7 plus-control">
                            <select class="form-control" name="ContractTypeID" id="ContractTypeID">
                           </select> 
                             <span class="glyphicon pls-icon glyphicon-minus remove-block" ></span>                      
                    </div>


                </div>`;

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
                                <label class="col-sm-5 control-label">Customer Description</label>

                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" ID="CustomerDescription" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-5 control-label">Customer Type</label>
                                <div class="col-sm-7 plus-control" id="non-edited-form">
                                    <asp:DropDownList ID="CustomerType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-5 control-label">
                                   IsPerson
                                </label>
                                <div class="col-sm-7">
                                    <asp:CheckBox runat="server" ID="IsPerson" ></asp:CheckBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-5 control-label">
                                 Submission Deadline
                                </label>
                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" TextMode="Number" ID="SubmissionDeadline" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-5 control-label">
                                 Personal Number ID
                                </label>
                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" ID="PersonalNumberID" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-5 control-label">
                                   Tax Number
                                </label>
                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" TextMode="Number" ID="TaxNumber" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-5 control-label">
                                 Bank Account Number
                                </label>
                                <div class="col-sm-7">
                                    <asp:TextBox runat="server"  ID="BankAccountNumber" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!-- /form -->

                    </div>

                   <div class="col-md-5 ">

                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-6 control-label">
                                  Address
                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:TextBox runat="server" ID="Address" CssClass="form-control" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-6 control-label">
                                   Phone Number
                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:TextBox runat="server" ID="PhoneNumber" TextMode="Phone"  CssClass="form-control"/>
                                </div>
                            </div>
                            <div class="form-group ">
                                <label class="col-sm-6 control-label">
                                  E-mail
                                </label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email"/>
                                </div>
                            </div>

                            <div class="form-group ">
                                <label class="col-sm-6 control-label">
                                  ContactPerson
                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:TextBox runat="server" ID="ContactPerson" CssClass="form-control" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-6 control-label">
                                ID Numebr
                                </label>
                                <div class="col-sm-6 chcbox">
                                    <asp:TextBox runat="server" ID="IDNumebr" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-6 control-label">
                                    Issued By
                                </label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" ID="IssuedBy" CssClass="form-control" />
                                </div>
                            </div>
                            
                           
                        </div>
                    </div>
                </div>
            </div>
            <br />
        <div class="">
            <asp:Button ID="save" runat="server" CssClass="btn btn-primary" OnClick="save_Click" Text="Save"/>
            <asp:Button ID="Update" runat="server" CssClass="btn btn-primary" OnClick="Update_Click" Text="Update" />
        </div>
        </main>
        <hr>
    </div>
</asp:Content>

