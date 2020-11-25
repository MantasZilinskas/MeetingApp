import axios from 'axios';
axios.defaults.baseURL = 'https://meetingappapiweb20200928134000.azurewebsites.net/api';
axios.defaults.headers = { Accept: `application/json` };

export const updateDefaultHeaders = (headers) => {
  axios.defaults.headers.common = {
    ...axios.defaults.headers.common,
    ...headers,
  };
};

const performRequest = async (config) => {
  const response = await axios.request(config);
  return response.data;
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
