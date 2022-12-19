import { Component } from "react";
import { Formik, Field, Form, ErrorMessage } from "formik";
import { Navigate } from "react-router-dom";
import * as Yup from "yup";
import "../styles/RegisterPage.css";

import UserService from "../services/UserService";

type Props = {};

type State = {
  redirect:  string | null,
  login: string,
  password: string,
  retryPassword: string,
  successful: boolean,
  message: string
};

export default class Register extends Component<Props, State> {
  constructor(props: Props) {
    super(props);
    this.handleRegister = this.handleRegister.bind(this);

    this.state = {
      redirect: null,
      login: "",
      password: "",
      retryPassword: "",
      successful: false,
      message: ""
    };
  }

  // todo: match passwords
  validationSchema() {
    return Yup.object().shape({
      /*username: Yup.string()
        .test(
          "len",
          "The username must be between 3 and 20 characters.",
          (val: any) =>
            val &&
            val.toString().length >= 3 &&
            val.toString().length <= 20
        )
        .required("This field is required!"),*/
      login: Yup.string()
        .test("len", "Login must be between 3 and 40 characters",
            (val: any) => val && val.toString().length >= 3 &&
                          val.toString().length <= 40)
        .required("This field is required!"),
      password: Yup.string()
        .test(
          "len",
          "The password must be between 3 and 40 characters.",
          (val: any) =>
            val &&
            val.toString().length >= 3 &&
            val.toString().length <= 40 
        )
        .required("This field is required!"),
      /*retryPassword: Yup.string()
          .test(
            "Passwords don't match!",
            (val: any) =>
              val.toString() == Yup.ref("password")
          )*/
    });
  }

  handleRegister(formValue: { login: string; password: string }) {

    console.log(formValue);

    const { login, password } = formValue;

    this.setState({
      message: "",
      successful: false
    });

    UserService.register(
        login,
        password
    ).then(
      response => {
        this.setState({
          message: response.data.message,
          successful: true
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
          successful: false,
          message: resMessage
        });
      }
    );

   this.toLoginPage();
  }

  toLoginPage = () => {
    this.setState({redirect: "/login"});
  }

  log() {
    console.log("click")
  }


  // todo: поправить validation schema и совпадение паролей и json отправляемый в post
  render() {

    if (this.state.redirect) {
        return <Navigate to={this.state.redirect} />
    }

    const { successful, message } = this.state;

    const initialValues = {
      login: "",
      password: "",
      retryPassword: ""
    };

    return (
      <div className="reg col-md-12">
        <tr>
        <td> 
            <button className="btn-back" onClick={this.toLoginPage}>
                <img className="mbti-img mt-5 mr-5" width="50"
                        src={require("../img/back.png")}/>
            </button>
        </td>
        <td>
            <h2 className="mbti-world mt-1 ml-3 mb-5">
                <text className="text">Get started!</text>
            </h2>
            </td>
        </tr>
        
        <div>
          <Formik
            initialValues={initialValues}
            validationSchema={this.validationSchema}
            onSubmit={this.handleRegister}
          >
            <Form>
              {!successful && (
                <div>
                  <div className="form-group">
                    <label htmlFor="login"> Login </label>
                    <Field name="login" type="login" className="form-control" />
                    <ErrorMessage
                      name="login"
                      component="div"
                      className="alert alert-danger"
                    />
                  </div>

                  <div className="form-group">
                    <label htmlFor="password"> Password </label>
                    <Field
                      name="password"
                      type="password"
                      className="form-control"
                    />
                    <ErrorMessage
                      name="password"
                      component="div"
                      className="alert alert-danger"
                    />
                  </div>

                  <div className="form-group">
                    <label htmlFor="retryPassword"> Retry password </label>
                    <Field
                      name="retryPassword"
                      type="password"
                      className="form-control"
                    />
                    <ErrorMessage
                      name="retryPassword"
                      component="div"
                      className="alert alert-danger"
                    />
                  </div>

                  <div className="form-group">
                        <button 
                            type="submit" 
                            className="btn btn-primary btn-block bg-warning mt-4"
                            >
                        <text className="button-text">Register</text>
                        </button>
                    </div>
                </div>
              )}

              {message && (
                <div className="form-group">
                  <div
                    className={
                      successful ? "alert alert-success" : "alert alert-danger"
                    }
                    role="alert"
                  >
                    {message}
                  </div>
                </div>
              )}
            </Form>
          </Formik>
        </div>
      </div>
    );
  }
}