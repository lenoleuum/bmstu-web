import { Link } from "react-router-dom";
import UserService from "../services/UserService";

import "../App.css";

const NavigationBar = () => {
    return (
        <div>
            <nav className="navbar navbar-expand navbar-light bg-warning">
                <img className="img-responsive pr-2 " width="40" src={require("../img/mbti.png")} />
                <a href="/home" className="navbar-brand">
                MBTIworld
                </a>
                <div className="navbar-nav px-ml-15 ml-auto">
                <li className="nav-item">
                    <Link to={"/home/types"} className="nav-link">
                    16 types
                    </Link>
                </li>
                </div>
                <div className="navbar-nav px-mr-5 ml-auto">
                <li className="nav-item">
                    <Link to={"/home/test"} className="nav-link">
                    personality test
                    </Link>
                </li>
                </div>
                <div className="navbar-nav px-mr-5">
                <li className="nav-item">
                    <Link to={"/home/characters"} className="nav-link">
                    characters
                    </Link>
                </li>
                </div>
                
                <a href="/login">
                <button 
                    className="btn-logout mr-4 bg-waring" 
                    onClick={UserService.logout}>
                     
                    <img className="mbti-img" width="30"
                        src={require("../img/logout.png")}/>
                </button>
                </a>

                <a href="/profile" className="navbar-brand">
                <img 
                    className="img-responsive ml-2 pr-2 ml-auto" 
                    width="40" 
                    src={require("../img/profile-transformed.png")} />
                </a>
        </nav>
      </div>
    )
}

export default NavigationBar;