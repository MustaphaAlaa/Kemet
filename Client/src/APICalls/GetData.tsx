import { useEffect, useState } from "react";
import type { APIResponse } from "../app/Models/APIResponse";
import axios from "axios";




export default function GetData<T>(url: string) {
  const [response, setResponse] = useState<APIResponse<T>>();


  const getData = async () => {
    try {
      const { data } = await axios.get<APIResponse<T>>(url);
      setResponse(data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  }

  useEffect(() => {
    getData()
  }, []);


  useEffect(() => {
    if (response) {
      console.log('Updated response:', response);
    }
  }, [response]);
  return {
    response,
    data: response?.result as T
  }
}