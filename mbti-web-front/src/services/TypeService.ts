import axios from "axios";
import authHeader from "../helpers/AuthHeader";
import IType from "../types/Type";

class TypeService {
  API_URL: string = "http://localhost:7026/api/types/";

  getAll() {
    return axios.get(this.API_URL, {headers: authHeader()});
  }

  findByName(name: string) {
    return axios.get(this.API_URL + "?name=" + name, {headers: authHeader()});
  }

  update(data: IType) {
    return axios.patch(this.API_URL, data, {headers: authHeader()});
  }
}

export default new TypeService();