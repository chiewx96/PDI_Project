const base_url = process.env.VUE_APP_BASEURL + "/api/";

const _post = async function (url, body) {
  return FetchFunc("POST", base_url + url, body);
};

const _get = async function (url, body) {
  return FetchFunc("GET", base_url + url, body);
};

const _put = async function (url, body) {
  return FetchFunc("PUT", base_url + url, body);
};

const _delete = async function (url, body) {
  return FetchFunc("DELETE", base_url + url, body);
};

const FetchFunc = async function (type, url, body) {
  return fetch(url, {
    method: type,
    headers: {
      // Authorization: 'Bearer ' + this.token,
      Accept: "application/json",
      "Content-Type": "application/json;charset=utf-8",
    },
    body: body != null ? JSON.stringify(body) : null,
  });
};

export default {
  _post,
  _get,
  _put,
  _delete,
};