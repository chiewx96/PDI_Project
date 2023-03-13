import { createApp } from "vue";
import App from "./App.vue";
import "./registerServiceWorker";
import router from "./router";
// import ApiService from "@/services/api.service.js";

// ApiService.init();

createApp(App).use(router).mount("#app");
