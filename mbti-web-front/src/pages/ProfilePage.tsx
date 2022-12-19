import { Component } from "react";
import { Navigate } from "react-router-dom";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";

import UserService from "../services/UserService";
import IUser from "../types/User";

type Props = {};

type State = {
    redirect: string | null,
    userReady: boolean,
    currentUser: IUser
  }

export default class ProfilePage extends Component<Props, State> {
    constructor(props: Props) {
        super(props);

        this.handleChangeProfile = this.handleChangeProfile.bind(this);

        this.state = {
            redirect: null,
            userReady: false,
            currentUser: UserService.getCurrentUserInfo()
          };
    }

    // todo: validation for email and dateOfBirth
    validationSchema() {
        return Yup.object().shape({
          email: Yup.string().required("This field is required!"),
          name: Yup.string().required("This field is required!"),
          dateOfBirth: Yup.string().required("This field is required!"),
        });
      }

    componentDidMount(): void {
        const curUser = UserService.getCurrentUserInfo();

        if (!curUser) 
            this.setState({ redirect: "/login" });

        this.setState({ currentUser: curUser, userReady: true,});        
    }

    checkSelected(type: string) : boolean {
      if (this.state.currentUser.typeuk == type)
        return true;
      else
        return false;
    };

    componentWillUnmount() {
        
        window.location.reload();
      }

    handleChangeProfile(formValue: { name?: string; email?: string, dateOfBirth?: string}) {

      console.log(formValue.name, formValue.email, formValue.dateOfBirth);

      // todo: validation
      if (true) //(formValue.name != "" && formValue.email != "" && formValue.dateOfBirth != "")
      {
        var e = document.getElementById("selectvalue") as HTMLSelectElement;
        var type = e.value;

        const user: any = {
          "id": this.state.currentUser.id,
          "login": this.state.currentUser.login,
          "password": "",
          "nickname": formValue.name,
          "email": formValue.email,
          "telagram": this.state.currentUser.telagram,
          "typeuk": type,
          "dateofbirth":  (formValue.dateOfBirth == "" ? "0001-01-01" : formValue.dateOfBirth),
        }

        UserService.update(user); 

        const newuser: any = {
          "id": this.state.currentUser.id,
          "login": this.state.currentUser.login,
          "nickname": formValue.name,
          "email": formValue.email,
          "telagram": this.state.currentUser.telagram,
          "typeuk": type,
          "dateofbirth":  (formValue.dateOfBirth == "" ? "0001-01-01" : formValue.dateOfBirth),
          "token": this.state.currentUser.token
        }

        UserService.updateUser(newuser);
        
        this.toHomePage();
      }
    }

    toHomePage = () => {
        this.setState({redirect: "/home"});
    }

    // todo: options from list
    render () {
        if (this.state.redirect) {
            return <Navigate to={this.state.redirect} />
        }
      
        const initialValues = {
            name: this.state.currentUser.nickname,
            email: this.state.currentUser.email,
            dateOfBirth: (this.state.currentUser.dateofbirth == "0001-01-01" ? "" : this.state.currentUser.dateofbirth),
        };

        return (
          <div>
            <div className="rectangle text_center mb-3">
                        <br/>
                        <text>Lets get you set up!</text>
                        <br/>
                        <text className="text-small">it should only take a couple of minutes</text>
                    </div>
            <div className="col-md-6">
                    <div>
                    <button 
                        className="btn-back-profile" 
                        onClick={this.toHomePage}>
                        <img className="back-img" width="50"
                                src={require("../img/back.png")}/>
                    </button>
                    </div>
            
                    <Formik
            initialValues={initialValues}
            validationSchema={null}
            onSubmit={this.handleChangeProfile}
            
          >
            <Form>
              <div className="form-group">
                <label htmlFor="name">Name</label>
                <Field name="name" type="login" className="form-control" />
                <ErrorMessage
                  name="name"
                  component="div"
                  className="alert alert-danger"
                />
              </div>

              <div className="form-group">
                <label htmlFor="email">E-mail</label>
                <Field name="email" type="login" className="form-control" />
                <ErrorMessage
                  name="email"
                  component="div"
                  className="alert alert-danger"
                />
              </div>

              <div className="form-group">
                <label htmlFor="dateOfBirth">Date of Birth</label>
                <Field name="dateOfBirth" type="login" className="form-control" />
                <ErrorMessage
                  name="dateOfBirth"
                  component="div"
                  className="alert alert-danger"
                />
              </div>

              <div className="form-group mt-4">
              <label className="mr-5">Your <text className="text-warning">MBTI</text> type is </label>
              <select id="selectvalue">
                <option value="0" selected={this.state.currentUser.typeuk == ""}>
                </option>
                <option value="ENTP" selected={this.checkSelected("ENTP")}>ENTP</option>
                <option value="ENTJ" selected={this.checkSelected("ENTJ")}>ENTJ</option>
                <option value="INTJ" selected={this.checkSelected("INTJ")}>INTJ</option>
                <option value="INTP" selected={this.checkSelected("INTP")}>INTP</option>
                <option value="ENFP" selected={this.checkSelected("ENFP")}>ENFP</option>
                <option value="ENFJ" selected={this.checkSelected("ENFJ")}>ENFJ</option>
                <option value="INFP" selected={this.checkSelected("INFP")}>INFP</option>
                <option value="INFJ" selected={this.checkSelected("INFJ")}>INFJ</option>
                <option value="ESFJ" selected={this.checkSelected("ESFJ")}>ESFJ</option>
                <option value="ESTJ" selected={this.checkSelected("ESTJ")}>ESTJ</option>
                <option value="ISFJ" selected={this.checkSelected("ISFJ")}>ISFJ</option>
                <option value="ISTJ" selected={this.checkSelected("ISTJ")}>ISTJ</option>
                <option value="ESFP" selected={this.checkSelected("ESFP")}>ESFP</option>
                <option value="ESTP" selected={this.checkSelected("ESTP")}>ESTP</option>
                <option value="ISFP" selected={this.checkSelected("ISFP")}>ISFP</option>
                <option value="ISTP" selected={this.checkSelected("ISTP")}>ISTP</option>
                
              </select>
              </div>

              <div className="form-group mt-5">
                <button 
                    type="submit" 
                    className="btn btn-primary btn-block bg-warning">
                  <span className="button-text">Save changes</span>
                </button>
              </div>
              
            </Form>
          </Formik>
            </div>
            </div>
        )
    }
}