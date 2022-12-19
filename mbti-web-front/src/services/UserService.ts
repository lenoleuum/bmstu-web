import axios from "axios";
import authHeader from "../helpers/AuthHeader";

class UserService {
    API_URL: string = "http://localhost:7026/api/users/";

    async login(login: string, password: string) {
        return await axios
               .post(this.API_URL + "authenticate", 
               {
                    login,
                    password
               })
               .then(response => {
                if (response.data.token)
                    localStorage.setItem("user", JSON.stringify(response.data));
                return response.data;
               })
    }

    logout() {
        localStorage.removeItem("user");
    }

    async register(login: string, password: string) {
        return await axios.post(this.API_URL + "register",
        {
            login,
            password
        });
    }

    getCurrentUserInfo()
    {
        var curUser = localStorage.getItem("user");

        if (curUser)    
            return JSON.parse(curUser);
        else    
            return null;
    }

    async update(user: any) {
        return await axios.patch(this.API_URL + user.id, user, {headers: authHeader()});
    }

    updateUser(user: any) {
        localStorage.removeItem("user");
        localStorage.setItem("user", JSON.stringify(user));
    }
}

export default new UserService();