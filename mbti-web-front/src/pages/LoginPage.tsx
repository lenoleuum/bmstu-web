import { Component } from "react";
import { Navigate } from "react-router-dom";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import "../styles/LoginPage.css";

import UserService from "../services/UserService";
  
type Props = {};

type State = {
  redirect: string | null,
  username: string,
  password: string,
  loading: boolean,
  message: string
};
  
export default class LoginPage extends Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.handleLogin = this.handleLogin.bind(this);
    
        this.state = {
          redirect: null,
          username: "",
          password: "",
          loading: false,
          message: ""
        };
    }
  
    componentDidMount() {
        const currentUser = UserService.getCurrentUserInfo();
    
        if (currentUser) {
          this.setState({ redirect: "/home" });
        };
      }
    
    componentWillUnmount() {
        window.location.reload();
    }
    
      validationSchema() {
        return Yup.object().shape({
          login: Yup.string().required("This field is required!"),
          password: Yup.string().required("This field is required!"),
        });
      }
    
      handleLogin(formValue: { login: string; password: string }) {
        const { login, password } = formValue;
    
        this.setState({
          message: "",
          loading: true
        });
  
        UserService.login(login, password).then(
            () => {
                this.setState({
                  redirect: "/home"
                });
        },
        error => {
          const resMessage =
            (error.response &&
              error.response.data &&
              error.response.data.message) ||
            error.message ||
            error.toString();
  
          this.setState({
            loading: false,
            message: resMessage
          });
        }
      );
    }

    toRegisterPage = () => {
        this.setState({redirect: "/register"});
    }
  
    render () {

        if (this.state.redirect) {
            return <Navigate to={this.state.redirect} />
          }
      
        const { loading, message } = this.state;
  
        const initialValues = {
            login: "",
            password: "",
        };

        return (
        <div className="log col-md-12">
        <div>
            <h2 className="mbti-world mt-5 mb-3">
                <text className="text-warning">MBTI</text>world 
            </h2>
          <img className="mbti-img mt-3 mb-4" width="80"
            src={require("../img/mbti_yellow.png")}/>
          <Formik
            initialValues={initialValues}
            validationSchema={this.validationSchema}
            onSubmit={this.handleLogin}
            
          >
            <Form>
              <div className="form-group">
                <label htmlFor="login">Login</label>
                <Field name="login" type="login" className="form-control" />
                <ErrorMessage
                  name="login"
                  component="div"
                  className="alert alert-danger"
                />
              </div>

              <div className="form-group">
                <label htmlFor="password">Password</label>
                <Field name="password" type="password" className="form-control" />
                <ErrorMessage
                  name="password"
                  component="div"
                  className="alert alert-danger"
                />
              </div>
              <div className="form-group">
                <button 
                    type="submit" 
                    className="btn btn-primary btn-block bg-warning"
                    disabled={loading}>
                  {loading && (
                    <span className="spinner-border spinner-border-sm"></span>
                  )}
                  <span className="button-text">Login</span>
                </button>
              </div>
              <div className="form-group">
                <button  
                    className="btn btn-primary btn-block" 
                    onClick={this.toRegisterPage}
                    >
                  <span className="button-text">Sign up</span>
                </button>
              </div>
            </Form>
          </Formik>
          <text className="text-password">
          forgot password? your problem...
          </text>
        </div>
      </div>
    )
    }
}