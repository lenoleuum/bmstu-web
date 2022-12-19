import React, { useState, useEffect, ChangeEvent } from "react";
import TypeService from "../services/TypeService";
import IType from "../types/Type";
import NavigationBar from "./NavigationBar";

import "../App.css";

// todo: with Component<Props, State>
const TypesList: React.FC = () => {
  const [types, setTypes] = useState<Array<IType>>([]);
  const [currentType, setCurrentType] = useState<IType | null>(null);
  const [currentIndex, setCurrentIndex] = useState<number>(-1);
  const [searchName, setsearchName] = useState<string>("");

  useEffect(() => {
    retrieveTypes();
  }, []);

  const onChangeSearchName = (e: ChangeEvent<HTMLInputElement>) => {
    const searchName = e.target.value;
    setsearchName(searchName);
  };

  const retrieveTypes = () => {
    TypeService.getAll()
      .then((response: any) => {
        setTypes(response.data);
      })
      .catch((e: Error) => {
        console.log(e);
      });
  };

  const refreshList = () => {
    retrieveTypes();
    setCurrentType(null);
    setCurrentIndex(-1);
  };

  const setActiveType = (type: IType, index: number) => {
    setCurrentType(type);
    setCurrentIndex(index);
  };

  const findByName = () => {
    if (searchName == "")
    {
      refreshList();
    }
    else
    {
      TypeService.findByName(searchName)
          .then((response: any) => {
            setTypes(response.data);
            setCurrentType(response.data[1]);
            setCurrentIndex(response.data[1].id);
          })
          .catch((e: Error) => {
            console.log(e);
          });
    }
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
            placeholder="search type by name"
            value={searchName}
            onChange={onChangeSearchName}
          />
          <div className="input-group-append w-300">
            <button
              className="btn btn-outline-secondary ml-1"
              type="button"
              onClick={findByName}
            >
              Search
            </button>
          </div>
        </div>
      </div>
      <div className="col-md-6">
        <ul className="list-group mt-5">
          {types &&
            types.map((type, index) => (
              <li
                className={
                  "list-group-item " + (index === currentIndex ? "active" : "")
                }
                onClick={() => setActiveType(type, index)}
                key={index}
              >
                {type.name}
              </li>
            ))}
        </ul>
      </div>
      <div className="type col-md-6 mt-5">
        {currentType ? (
          <div>
            <div>
              <label>
                <strong>Name:</strong>
              </label>{" "}
              {currentType.name}
            </div>
            <div>
              <label>
                <strong>Description:</strong>
              </label>{" "}
              {currentType.description}
            </div>
          </div>
        ) : (
          <div>
            <br />
            <p>Here you can find info about 16 mbti types.</p>
            <p><text className="text text-warning">Click</text> at type to see info...</p>
          </div>
        )}
      </div>
    </div>
    </div>
  );
};

export default TypesList;