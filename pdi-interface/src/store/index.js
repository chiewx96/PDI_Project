import router from "@/router";
import { createStore } from "vuex";

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
    // logout(state){
    //   state.token = null;
    //   state.user = null;
    // }
  },
  actions: {
    logout(){
      this.state.token = null;
      this.state.user = null;
      router.push('/');
    }
  },
  modules: {},
});
