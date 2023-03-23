import { createStore } from "vuex";
// import Vue from "vue";
// import Vuex from "vuex";

// Vue.use(Vuex);

export default createStore({
  state: {
    user: null,
    token: null,
  },
  getters: {
    isLoggedIn(state) {
      return state.token != null;
      // return !!state.token;
    },
    getToken(state){
      return state.token;
    },
    getUser(state){
      return state.user;
    }
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
    },
    setToken(state, token) {
      state.token = token;
    },
  },
  actions: {},
  modules: {},
});
