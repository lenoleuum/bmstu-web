import axios from "axios";
import ICharaceter from "../types/Character";
import authHeader from "../helpers/AuthHeader";

class CharacterService {
    API_URL: string = "http://localhost:7026/api/characters/";

    getAll() {
        return axios.get(this.API_URL, {headers: authHeader()});
    }

    findByType(type: string) {
        return axios.get(this.API_URL + "?str=" + type + "&flag=1", {headers: authHeader()});
    } 

    findByCategory(category: string) {
        return axios.get(this.API_URL + "?str=" + category + "&flag=2", {headers: authHeader()});
    } 

    findByName(name: string) {
        return axios.get(this.API_URL + "?str=" + name + "&flag=3", {headers: authHeader()});
    } 
}
  
export default new CharacterService();