import store from "@/store";

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
  let timeout = 20000;
  const options = { timeout: timeout };
  const controller = new AbortController();
  const id = setTimeout(() => controller.abort(), timeout);

  let response_data = {
    method: type,
    headers: {
      // Authorization: 'Bearer ' + this.token,
      Accept: "application/json",
      "Content-Type": "application/json;charset=utf-8",
    },
    body: body != null ? JSON.stringify(body) : null,
    ...options,
    signal: controller.signal,
  };
  // if (store.getters.getToken) {
  if (store.getters.isLoggedIn) {
    response_data.headers.Authorization = "Bearer " + store.getters.getToken;
  }
  var response = await fetch(url, response_data);
  clearTimeout(id);
  return response;
};

export default {
  _post,
  _get,
  _put,
  _delete,
};
