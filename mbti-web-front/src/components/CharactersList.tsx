import React, { useState, useEffect, ChangeEvent } from "react";

import CharacterService from '../services/CharacterService';
import ICharacter from "../types/Character";
import NavigationBar from "./NavigationBar";

// todo: with Component<Props, State>
const CharacterItem = () => {
    const [characters, setCharacters] = useState<ICharacter[]>([]);
    const [currentCharacter, setCurrentCharacter] = useState<ICharacter | null>(null);
    const [currentIndex, setCurrentIndex] = useState<number>(-1);
    const [searchStr, setsearchStr] = useState<string>("");
    const [searchParam, setSearchParam] = useState<string>("");

    useEffect(() => {
      retrieveCharacters();
    }, []);

    const onChangeSearchName = (e: ChangeEvent<HTMLInputElement>) => {
      const searchStr = e.target.value;
      setsearchStr(searchStr);
    };

    const retrieveCharacters = () => {
      CharacterService.getAll()
        .then((response: any) => {
          setCharacters(response.data);
          console.log(response.data);
        })
        .catch((e: Error) => {
          console.log(e);
        });
    };
  
    const refreshList = () => {
      retrieveCharacters();
      setCurrentCharacter(null);
      setCurrentIndex(-1);
    };

    const setActiveCharacter = (type: ICharacter, index: number) => {
      setCurrentCharacter(type);
      setCurrentIndex(index);
    };

    const findByStr = () => {
      switch(searchParam) {
        case "1":
          if (searchStr == "")
          {
            refreshList();
          }
          else
          {
            CharacterService.findByCategory(searchStr)
                .then((response: any) => {
                  setCharacters(response.data);
                  setCurrentCharacter(response.data[1]);
                  setCurrentIndex(response.data[1].id);
                  console.log(response.data);
                })
                .catch((e: Error) => {
                  console.log(e);
                });
          }
          break;

        case "2":
          if (searchStr == "")
          {
            refreshList();
          }
          else
          {
            CharacterService.findByName(searchStr)
                .then((response: any) => {
                  setCharacters(response.data);
                  setCurrentCharacter(response.data[1]);
                  setCurrentIndex(response.data[1].id);
                  console.log(response.data);
                })
                .catch((e: Error) => {
                  console.log(e);
                });
          }
          break;

        case "3":
          if (searchStr == "")
          {
            refreshList();
          }
          else
          {
            CharacterService.findByType(searchStr)
                .then((response: any) => {
                  setCharacters(response.data);
                  setCurrentCharacter(response.data[1]);
                  setCurrentIndex(response.data[1].id);
                  console.log(response.data);
                })
                .catch((e: Error) => {
                  console.log(e);
                });
          }
          break;

        default:
          break;
      }
    };

    const onChangeSearchParam = (e: ChangeEvent<HTMLSelectElement>) => {
      const searchParam = e.target.value;
      setSearchParam(searchParam);
    };

    return (
      <div>
    <NavigationBar></NavigationBar>
    <div className="list row">
      <div className="col-md-8">
        <div className="input-group mt-5">
          <input
            type="text"
            className="form-control center-block p"
            placeholder="search in characters"
            value={searchStr}
            onChange={onChangeSearchName}
          />
          <select 
            className="drop-menu ml-1" 
            id="searchParam" 
            value={searchParam}
            onChange={onChangeSearchParam}>
                    <option value="1">Category</option>
                    <option value="2">Name</option>
                    <option value="3">Type</option>
                </select>
          <div className="input-group-append w-300">
            <button
              className="btn btn-outline-secondary ml-1"
              type="button"
              onClick={findByStr}
            >
              Search
            </button>
          </div>
        </div>
      </div>
      <div className="col-md-6">
        <ul className="list-group mt-5">
          {characters &&
            characters.map((character, index) => (
              <li
                className={
                  "list-group-item " + (index === currentIndex ? "active" : "")
                }
                onClick={() => setActiveCharacter(character, index)}
                key={index}
              >
                {character.name}
                <button
                  className="btn-type ml-5"
                  type="button"
                >
                  {character.type}
                </button>
              </li>
            ))}
        </ul>
      </div>
      <div className="type col-md-6 mt-5">
        {currentCharacter ? (
          <div>
            <div>
              <label>
                <strong>Name:</strong>
              </label>{" "}
              {currentCharacter.name}
            </div>
            <div>
              <label>
                <strong>MBTI type:</strong>
              </label>{" "}
              {currentCharacter.type}
            </div>
            <div>
              <label>
                <strong>Category:</strong>
              </label>{" "}
              {currentCharacter.category}
            </div>
          </div>
        ) : (
          <div>
            <br />
            <p>Here you can find information about characters and their mbti types.</p>
            <p><text className="text text-warning">Click</text> at character to see info...</p>
          </div>
        )}
      </div>
    </div>
    </div>
    );
}

export default CharacterItem;