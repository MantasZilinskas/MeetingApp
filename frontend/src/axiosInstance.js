import axios from 'axios';
import { auth } from './Utils/authenticationService';

axios.defaults.baseURL =
  'https://meetingappapiweb20200928134000.azurewebsites.net/api/';
// axios.defaults.baseURL =
//    'https://localhost:44335/api/';
axios.defaults.headers = {
  Accept: `application/json`,
};

export const updateDefaultHeaders = (headers) => {
  axios.defaults.headers.common = {
    ...axios.defaults.headers.common,
    ...headers,
  };
};

export default function authHeader() {
  const currentUser = auth.currentUserValue;
  if (currentUser && currentUser.token) {
      return { Authorization: `Bearer ${currentUser.token}` };
  } else {
      return {};
  }
}

const performRequest = async (config) => {
  try {
    const headers = authHeader();
    config = {
      ...config,
      headers,
    };
    const response = await axios.request(config);
    return response.data;
  } catch (error) {
    console.log(error.message);
  }
};

const performGet = (url, params = {}, config = {}) =>
  performRequest({
    ...config,
    method: 'get',
    params,
    url,
  });

const performPatch = (url, params = {}, config = {}) =>
  performRequest({
    ...config,
    method: 'patch',
    params,
    url,
  });

const performPost = (url, data = {}, params = {}, config = {}) =>
  performRequest({
    ...config,
    method: 'post',
    params,
    url,
    data,
  });

const performDelete = (url, params = {}, config = {}) =>
  performRequest({
    ...config,
    method: 'delete',
    params,
    url,
  });

const performPut = (url, data = {}, params = {}, config = {}) =>
  performRequest({
    ...config,
    method: 'put',
    params,
    url,
    data,
  });

export const api = {
  delete: performDelete,
  get: performGet,
  post: performPost,
  put: performPut,
  patch: performPatch,
};
