import React from "react";
import { Component } from "react";
import { Routes, Route, Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";

import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import PersonalityTest from "./components/PersonalityTest";
import TypesList from "./components/TypesList";
import CharacterItem from "./components/CharactersList";
import ProfilePage from "./pages/ProfilePage";

import UserService from "./services/UserService";
import IUser from "./types/User";

import EventBus from "./helpers/EventBus";

type Props = {};

type State = {
  currentUser: IUser | undefined
}

export default class App extends Component<Props, State> {
  constructor(props: Props) {
    super(props);
  }

  componentDidMount() {
    const user = UserService.getCurrentUserInfo();

    if (user) {
      this.setState({
        currentUser: user,
      });
    }

    EventBus.on("logout", this.logOut);
  }
  
  componentWillUnmount() {
    EventBus.remove("logout", this.logOut);
  }

  logOut() {
    UserService.logout();
    this.setState({
      currentUser: undefined,
    });
  }

  render () {

    return (
      <div>
        <div className="container mt-3">
        <Routes>
            <Route path="/" element={<LoginPage />} />
            <Route path="/home" element={<PersonalityTest />} />
            <Route path="/home/types" element={<TypesList/>} />
            <Route path="/home/test" element={<PersonalityTest/>}/>
            <Route path="/home/characters" element={<CharacterItem/>}/>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/profile" element={<ProfilePage />} />
          </Routes>
        </div>
      </div>
    )
  }

}