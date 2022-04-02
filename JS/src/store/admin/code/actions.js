import axios from '@boot/axios';

const apiUrlPrefix = '/api/Code'; // COPYDANIEL: CamelCase TYPE1

export const fetchItems = ({ rootGetters, commit }, data) => {
  /**
   * Request body:
   *  None
   */
  const opts = {
    method: 'get',
    url: `${apiUrlPrefix}/query`,
    data: {
      ...rootGetters['app/getDefaultListParams'],
      ...data,
    },
  };

  const response = axios.webapi({ opts, commit }).then((res) => {
    commit('setItems', res.data || []);
    return res;
  });
  return response;
};

export const fetchItemById = ({ commit }, seqNo) => {
  /**
   * Request body:
   *  seqNo : Integer *Required
   */
  const opts = {
    method: 'get',
    url: `${apiUrlPrefix}/${seqNo}`,
  };

  const response = axios.webapi({ opts, commit }).then((res) => {
    commit('setItem', res.data);
    return res.data;
  });

  return response;
};

export const fetchCreate = ({ commit }, data) => {
  /**
   * Request body:
   * extCode      : String,
   * computerName : String,
   * computerIp   : String,
   * memo         : String,
   * isEnable     : Boolean
   */
  const opts = {
    method: 'post',
    url: `${apiUrlPrefix}`,
    data,
  };

  const response = axios.webapi({ opts, commit }).then((res) => res);

  return response;
};

export const fetchUpdate = ({ commit }, data) => {
  /**
   * Request body:
   * seqNo        : Integer,
   * extCode      : String,
   * computerName : String,
   * computerIp   : String,
   * memo         : String,
   * isEnable     : Boolean
   */

  const opts = {
    method: 'put',
    url: `${apiUrlPrefix}`,
    data,
  };

  const response = axios.webapi({ opts, commit }).then((res) => res);

  return response;
};

export const fetchDelete = ({ commit }, seqNo) => {
  /**
   * Request body:
   *  seqNo : Integer
   */
  const opts = {
    method: 'delete',
    url: `${apiUrlPrefix}`,
    data: {
      seqNo: seqNo,
    },
  };

  const response = axios.webapi({ opts, commit }).then((res) => res);

  return response;
};
